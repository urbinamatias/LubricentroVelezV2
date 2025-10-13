using LubricentroVelezV2.DTOs;
using LubricentroVelezV2.Models;
using LubricentroVelezV2.Repositories.Interface;
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
    }
}
