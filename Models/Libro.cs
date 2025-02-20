using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKER_GESTION_BIBLIOTECA.Models
{
    public class Libro
    {
        public long idLibro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public int copiasDisponibles { get; set; }
        public char estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime? fechaModificacion { get; set; }
    }

}
