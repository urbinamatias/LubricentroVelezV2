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
    }
}
