using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKER_GESTION_BIBLIOTECA.Models
{
    public class Historial
    {
        public long idHistorial { get; set; }
        public long idPrestamo { get; set; }
        public string tipoOperacion { get; set; }
        public DateTime fechaOperacion { get; set; }
        public string detalles { get; set; }

        // Propiedad de navegación 
        public Prestamo Prestamo { get; set; }
    }
}
