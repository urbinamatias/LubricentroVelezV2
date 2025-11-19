using LubricentroVelezV2.DTOs;
using LubricentroVelezV2.Models;
using LubricentroVelezV2.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LubricentroVelezV2.Repositories.Implementation
{
    public class OrdenesRepository : IOrdenesRepository
    {
        private readonly OrdenesContext _context;
        public OrdenesRepository(OrdenesContext context)
        {
            _context = context;
        }
        public async Task<List<OrdenesTrabajoDTO>> FillGridAsync()
        {
            return await _context.OrdenesTrabajos
                .OrderByDescending(ot => ot.IdOt)
                .Select(ot => new OrdenesTrabajoDTO
                {
                    IdOt = ot.IdOt,
                    Fecha = ot.Fecha,
                    Patente = ot.IdAutoNavigation.Patente,
                    Kilometraje = ot.Kilometraje,
                    Propietario = ot.IdAutoNavigation.IdPersonaNavigation.Nombre,
                    Aceite = ot.IdAceiteNavigation.Nombre,
                    Aditivo = ot.IdAditivoNavigation.Nombre
                }).ToListAsync();
        }
        public Task<List<OrdenesTrabajos>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<List<Aceites>> GetAceitesAsync()
        {
            return await _context.Aceites.OrderBy(a => a.Marca).ThenBy(a => a.Nombre).ToListAsync();
        }
        public async Task<List<Aditivos>> GetAditivosAsync()
        {
            return await _context.Aditivos.OrderBy(a => a.Nombre).ToListAsync();
        }

        public async Task AddAceiteAsync(Aceites aceite)
        {
            await _context.Aceites.AddAsync(aceite);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAceiteAsync(Aceites aceite)
        {
            try
            {
                var existing = await _context.Aceites.FindAsync(aceite.IdAceite);
                if (existing == null) throw new InvalidOperationException("El aceite no existe.");

                existing.Nombre = aceite.Nombre;
                existing.Marca = aceite.Marca;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                try { _context.ChangeTracker.Clear(); } catch { }
                throw;
            }
        }

        public async Task DeleteAceiteAsync(int id)
        {
            try
            {
                var item = await _context.Aceites.FindAsync(id);
                if (item == null) return;

                _context.Aceites.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try { _context.ChangeTracker.Clear(); } catch {  }

                var inner = ex.InnerException;
                if (inner != null && inner.Message.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "No se puede eliminar este Aceite porque está siendo utilizado en una o más órdenes de trabajo.");
                }
                throw;
            }
        }

        public async Task AddAditivoAsync(Aditivos aditivo)
        {
            await _context.Aditivos.AddAsync(aditivo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAditivoAsync(Aditivos aditivo)
        {
            try
            {
                var existing = await _context.Aditivos.FindAsync(aditivo.IdAditivo);
                if (existing == null) throw new InvalidOperationException("El aditivo no existe.");

                existing.Nombre = aditivo.Nombre;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                try { _context.ChangeTracker.Clear(); } catch { }
                throw;
            }
        }

        public async Task DeleteAditivoAsync(int id)
        {
            try
            {
                var item = await _context.Aditivos.FindAsync(id);
                if (item == null) return;

                _context.Aditivos.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                try { _context.ChangeTracker.Clear(); } catch { }

                var inner = ex.InnerException;
                if (inner != null && inner.Message.Contains("REFERENCE constraint", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "No se puede eliminar este Aditivo porque está siendo utilizado en una o más órdenes de trabajo.");
                }

                throw;
            }
        }
        public async Task<LastOrderDataDTO> GetLastOrderDataByPatenteAsync(string patente, CancellationToken cancellationToken = default)
        {
            // Busca la última orden de trabajo para esa patente
            var lastOrder = await _context.OrdenesTrabajos
                .Where(ot => ot.IdAutoNavigation.Patente.ToUpper() == patente.ToUpper())
                .OrderByDescending(ot => ot.Fecha) // Asume que la fecha es el mejor criterio para la "última" orden
                .Include(ot => ot.IdAutoNavigation)
                .ThenInclude(auto => auto.IdPersonaNavigation)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastOrder == null)
            {
                return null;
            }

            return new LastOrderDataDTO
            {
                PropietarioNombre = lastOrder.IdAutoNavigation.IdPersonaNavigation.Nombre,
                Telefono = lastOrder.IdAutoNavigation.IdPersonaNavigation.Telefono,
                Automovil = lastOrder.IdAutoNavigation.Vehiculo,
                Modelo = lastOrder.IdAutoNavigation.Año,
                IdAceite = lastOrder.IdAceite,
                IdAditivo = lastOrder.IdAditivo
            };
        }

        // Obtener detalles completos de una orden por ID
        public async Task<OrdenTrabajoDetailsDTO> GetOrderDetailsByIdAsync(int idOt)
        {
            var orden = await _context.OrdenesTrabajos
                .Where(ot => ot.IdOt == idOt)
                .Include(ot => ot.IdAutoNavigation)
                    .ThenInclude(auto => auto.IdPersonaNavigation)
                .FirstOrDefaultAsync();

            if (orden == null) return null;

            return new OrdenTrabajoDetailsDTO
            {
                IdOt = orden.IdOt,
                Kilometraje = orden.Kilometraje,
                IdAceite = orden.IdAceite,
                IdAditivo = orden.IdAditivo,
                FiltroAceite = orden.FiltroAceite,
                FiltroAire = orden.FiltroAire,
                FiltroCombustible = orden.FiltroCombustible,
                FiltroAbitaculo = orden.FiltroAbitaculo,
                Observaciones = orden.Observaciones,

                // Datos del Vehículo/Propietario
                Patente = orden.IdAutoNavigation.Patente,
                PropietarioNombre = orden.IdAutoNavigation.IdPersonaNavigation.Nombre,
                Telefono = orden.IdAutoNavigation.IdPersonaNavigation.Telefono,
                Automovil = orden.IdAutoNavigation.Vehiculo,
                Modelo = orden.IdAutoNavigation.Año
            };
        }

        // Guardar (Add) una nueva Orden de Trabajo
        public async Task<int> AddOrdenTrabajoAsync(OrdenTrabajoDetailsDTO ordenDTO)
        {
            // 1. Obtener o Crear Persona y Auto
            int idPersona = await GetOrCreatePersonaAsync(ordenDTO.PropietarioNombre, ordenDTO.Telefono);
            int idAuto = await GetOrCreateAutoAsync(ordenDTO.Patente, ordenDTO.Automovil, ordenDTO.Modelo, idPersona);

            // 2. Crear la Orden de Trabajo
            var nuevaOrden = new OrdenesTrabajos
            {
                Fecha = DateTime.Now,
                Kilometraje = ordenDTO.Kilometraje,
                IdAceite = ordenDTO.IdAceite,
                IdAditivo = ordenDTO.IdAditivo,
                IdAuto = idAuto, // Usamos el ID del Auto obtenido/creado
                FiltroAceite = ordenDTO.FiltroAceite ?? false,
                FiltroAire = ordenDTO.FiltroAire ?? false,
                FiltroCombustible = ordenDTO.FiltroCombustible ?? false,
                FiltroAbitaculo = ordenDTO.FiltroAbitaculo ?? false,
                Observaciones = ordenDTO.Observaciones,
                DeleteLogico = false,

                // Valores por defecto para campos no utilizados en el formulario
                FiltroHidraulico = false,
                FiltroAireSecundario = false,
                SecadorFrenos = false,
                FiltroAgua = false,
                TrampaAgua = false,
                AditivoCaja = false,
                AditivoDifTrasero = false,
                AditivoDifDelantero = false,
                AditivoCajaTransferencia = false,
            };

            // Rellenar con valores predeterminados (como string vacío) para campos string no utilizados si son NOT NULL en BD,
            // pero como son NULLABLE, podemos dejarlos en NULL (o string.Empty si prefieres). 
            // Para este ejemplo los dejaremos en NULL, excepto los booleanos que deben ser 'false'.

            await _context.OrdenesTrabajos.AddAsync(nuevaOrden);
            await _context.SaveChangesAsync();

            return nuevaOrden.IdOt;
        }

        // Actualizar (Update) una Orden de Trabajo
        public async Task UpdateOrdenTrabajoAsync(OrdenTrabajoDetailsDTO ordenDTO)
        {
            if (!ordenDTO.IdOt.HasValue) throw new InvalidOperationException("ID de Orden no especificado para la edición.");

            var ordenExistente = await _context.OrdenesTrabajos
                .Where(ot => ot.IdOt == ordenDTO.IdOt)
                .Include(ot => ot.IdAutoNavigation)
                .ThenInclude(auto => auto.IdPersonaNavigation)
                .FirstOrDefaultAsync();

            if (ordenExistente == null) throw new KeyNotFoundException("Orden de Trabajo no encontrada.");

            // 1. Obtener o Crear Persona y Auto (Puede que el propietario/teléfono/vehículo haya cambiado)
            int idPersona = await GetOrCreatePersonaAsync(ordenDTO.PropietarioNombre, ordenDTO.Telefono);
            int idAuto = await GetOrCreateAutoAsync(ordenDTO.Patente, ordenDTO.Automovil, ordenDTO.Modelo, idPersona);

            // 2. Actualizar la Orden
            ordenExistente.Kilometraje = ordenDTO.Kilometraje;
            ordenExistente.IdAceite = ordenDTO.IdAceite;
            ordenExistente.IdAditivo = ordenDTO.IdAditivo;
            ordenExistente.IdAuto = idAuto; // Posiblemente cambió
            ordenExistente.FiltroAceite = ordenDTO.FiltroAceite ?? false;
            ordenExistente.FiltroAire = ordenDTO.FiltroAire ?? false;
            ordenExistente.FiltroCombustible = ordenDTO.FiltroCombustible ?? false;
            ordenExistente.FiltroAbitaculo = ordenDTO.FiltroAbitaculo ?? false;
            ordenExistente.Observaciones = ordenDTO.Observaciones;

            // La fecha no se actualiza, es la fecha de creación.
            // Los campos no utilizados se mantienen como están (NULL o el valor por defecto/anterior).

            await _context.SaveChangesAsync();
        }

        // --- Métodos de ayuda (privados) para gestionar Persona y Auto ---

        private async Task<int> GetOrCreatePersonaAsync(string nombre, string telefono)
        {
            // Intentamos buscar una persona existente por nombre y teléfono
            var persona = await _context.Personas
                .FirstOrDefaultAsync(p => p.Nombre.ToUpper() == nombre.ToUpper() && p.Telefono == telefono);

            if (persona == null)
            {
                // Si no existe, creamos una nueva
                persona = new Personas
                {
                    Nombre = nombre,
                    Telefono = telefono
                };
                await _context.Personas.AddAsync(persona);
                await _context.SaveChangesAsync();
            }

            return persona.IdPersona;
        }

        private async Task<int> GetOrCreateAutoAsync(string patente, string vehiculo, int? año, int idPersona)
        {
            // Intentamos buscar un auto existente por patente
            var auto = await _context.Autos
                .FirstOrDefaultAsync(a => a.Patente.ToUpper() == patente.ToUpper());

            if (auto == null)
            {
                // Si no existe, creamos uno nuevo
                auto = new Autos
                {
                    Patente = patente,
                    Vehiculo = vehiculo,
                    Año = año,
                    IdPersona = idPersona
                };
                await _context.Autos.AddAsync(auto);
            }
            else
            {
                // Si existe, lo actualizamos con los últimos datos (vehículo y propietario)
                auto.Vehiculo = vehiculo;
                auto.Año = año;
                auto.IdPersona = idPersona;
                _context.Autos.Update(auto);
            }

            await _context.SaveChangesAsync();

            return auto.IdAuto;
        }
    }
}
