using LubricentroVelezV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LubricentroVelezV2.DTOs
{
    public class OrdenesTrabajoDTO
    {
        public int IdOt { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Patente { get; set; }
        public double? Kilometraje { get; set; }
        public string? Propietario { get; set; }
        public string? Aceite { get; set; }
        public string? Aditivo { get; set; }
    }
}
