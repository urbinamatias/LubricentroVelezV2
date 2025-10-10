using LubricentroVelezV2.DTOs;
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
        public Task<List<OrdenesTrabajoDTO>> FillGridAsync()
        {
            return _repository.FillGridAsync();
        }
    }
}
