using LubricentroVelezV2.DTOs;
using LubricentroVelezV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LubricentroVelezV2.Services.Implementation.OrdenesService;

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
        Task<List<AceiteComboItem>> GetAceitesForComboAsync();
        Task<List<AditivoComboItem>> GetAditivosForComboAsync();
        Task<LastOrderDataDTO> GetLastOrderDataByPatenteAsync(string patente, CancellationToken cancellationToken = default);
        Task<OrdenTrabajoDetailsDTO> GetOrderDetailsByIdAsync(int idOt);
        Task<int> AddOrdenTrabajoAsync(OrdenTrabajoDetailsDTO ordenDTO);
        Task UpdateOrdenTrabajoAsync(OrdenTrabajoDetailsDTO ordenDTO);
        Task DeleteOrdenTrabajoAsync(int idOt);
    }
}
