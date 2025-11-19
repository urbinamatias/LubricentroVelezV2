using LubricentroVelezV2.DTOs;
using LubricentroVelezV2.Models;
using LubricentroVelezV2.Repositories.Interface;
using LubricentroVelezV2.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LubricentroVelezV2.Services.Implementation
{
    public class OrdenesService : IOrdenesService
    {
        private readonly IOrdenesRepository _repository;
        public OrdenesService(IOrdenesRepository repository)
        {
            _repository = repository;
        }

        public async Task AddAceiteAsync(Aceites aceite)
        {
            await _repository.AddAceiteAsync(aceite);
        }

        public async Task AddAditivoAsync(Aditivos aditivo)
        {
            await _repository.AddAditivoAsync(aditivo);
        }

        public async Task DeleteAceiteAsync(int id)
        {
            await _repository.DeleteAceiteAsync(id);
        }

        public async Task DeleteAditivoAsync(int id)
        {
            await _repository.DeleteAditivoAsync(id);
        }

        public Task<List<OrdenesTrabajoDTO>> FillGridAsync()
        {
            return _repository.FillGridAsync();
        }
        public async Task<List<Models.Aceites>> GetAceitesAsync()
        {
            return await _repository.GetAceitesAsync();
        }
        public async Task<List<Models.Aditivos>> GetAditivosAsync()
        {
            return await _repository.GetAditivosAsync();
        }

        public async Task UpdateAceiteAsync(Aceites aceite)
        {
            await _repository.UpdateAceiteAsync(aceite);
        }

        public async Task UpdateAditivoAsync(Aditivos aditivo)
        {
            await _repository.UpdateAditivoAsync(aditivo);
        }
        public async Task<List<AceiteComboItem>> GetAceitesForComboAsync()
        {
            var aceites = await _repository.GetAceitesAsync();
            return aceites.Select(a => new AceiteComboItem
            {
                IdAceite = a.IdAceite,
                Display = string.IsNullOrEmpty(a.Marca) ? a.Nombre : $"{a.Nombre} - {a.Marca}"
            }).ToList();
        }

        // Método para obtener Aditivos
        public async Task<List<AditivoComboItem>> GetAditivosForComboAsync()
        {
            var aditivos = await _repository.GetAditivosAsync();
            return aditivos.Select(a => new AditivoComboItem
            {
                IdAditivo = a.IdAditivo,
                Display = a.Nombre
            }).ToList();
        }

        // Nuevos métodos del Servicio
        public Task<LastOrderDataDTO> GetLastOrderDataByPatenteAsync(string patente, CancellationToken cancellationToken = default)
        {
            return _repository.GetLastOrderDataByPatenteAsync(patente, cancellationToken);
        }

        public Task<OrdenTrabajoDetailsDTO> GetOrderDetailsByIdAsync(int idOt)
        {
            return _repository.GetOrderDetailsByIdAsync(idOt);
        }

        public Task<int> AddOrdenTrabajoAsync(OrdenTrabajoDetailsDTO ordenDTO)
        {
            return _repository.AddOrdenTrabajoAsync(ordenDTO);
        }

        public Task UpdateOrdenTrabajoAsync(OrdenTrabajoDetailsDTO ordenDTO)
        {
            return _repository.UpdateOrdenTrabajoAsync(ordenDTO);
        }

        // Clases de Ayuda (puedes definirlas fuera si prefieres)
        public class AceiteComboItem
        {
            public int IdAceite { get; set; }
            public string Display { get; set; }
        }

        public class AditivoComboItem
        {
            public int IdAditivo { get; set; }
            public string Display { get; set; }
        }
    }
}
