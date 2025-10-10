using LubricentroVelezV2.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LubricentroVelezV2.Services.Interface
{
    public interface IOrdenesService
    {
        Task<List<OrdenesTrabajoDTO>> FillGridAsync();
    }
}
