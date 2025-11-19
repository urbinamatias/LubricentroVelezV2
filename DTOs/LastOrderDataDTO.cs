using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LubricentroVelezV2.DTOs
{
    public class LastOrderDataDTO
    {
        // Datos del Propietario/Vehículo
        public string PropietarioNombre { get; set; }
        public string Telefono { get; set; }
        public string Automovil { get; set; } // Campo 'Vehiculo' en Autos
        public int? Modelo { get; set; }

        // Datos de Aceite/Aditivo
        public int? IdAceite { get; set; }
        public int? IdAditivo { get; set; }
    }
}
