using LubricentroVelezV2.DTOs;
using LubricentroVelezV2.Models;
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
        Task<List<Aceites>> GetAceitesAsync();
        Task<List<Aditivos>> GetAditivosAsync();
        Task AddAceiteAsync(Aceites aceite);
        Task UpdateAceiteAsync(Aceites aceite);
        Task DeleteAceiteAsync(int id);
        Task AddAditivoAsync(Aditivos aditivo);
        Task UpdateAditivoAsync(Aditivos aditivo);
        Task DeleteAditivoAsync(int id);
    }
}
