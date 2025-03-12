using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKER_GESTION_BIBLIOTECA.Models
{
    public class Prestamo
    {
        public long idPrestamo { get; set; }
        public long idLibro { get; set; }
        public long idUsuario { get; set; }
        public DateTime fechaPrestamo { get; set; }
        public DateTime fechaDevolucionEsperada { get; set; }
        public char estado { get; set; }

        // Propiedades de navegación
        public Libro Libro { get; set; }
        public Usuario Usuario { get; set; }
    }

    //public class InfoDevolucion
    //{
    //    public int IdPrestamo { get; set; }
    //    public string TituloLibro { get; set; }
    //    public string NombreUsuario { get; set; }
    //    public DateTime FechaDevolucion { get; set; }
    //    public int CopiasDisponibles { get; set; }
    //}
}
