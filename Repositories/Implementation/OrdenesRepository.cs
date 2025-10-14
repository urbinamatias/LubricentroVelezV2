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
    }
}
