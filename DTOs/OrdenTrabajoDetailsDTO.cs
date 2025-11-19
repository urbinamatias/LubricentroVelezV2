using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LubricentroVelezV2.DTOs
{
    public class OrdenTrabajoDetailsDTO
    {
        // Campos de la Orden de Trabajo (frmOrdenTrabajo)
        public int? IdOt { get; set; } // Nullable para nuevas órdenes
        public double? Kilometraje { get; set; }
        public int? IdAceite { get; set; }
        public int? IdAditivo { get; set; }
        public bool? FiltroAceite { get; set; }
        public bool? FiltroAire { get; set; }
        public bool? FiltroCombustible { get; set; }
        public bool? FiltroAbitaculo { get; set; }
        public string Observaciones { get; set; }

        // Campos del Vehículo/Propietario para la Interfaz
        public string Patente { get; set; }
        public string PropietarioNombre { get; set; }
        public string Telefono { get; set; }
        public string Automovil { get; set; } // Vehiculo
        public int? Modelo { get; set; }
    }
}
