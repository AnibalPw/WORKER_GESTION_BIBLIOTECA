using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKER_GESTION_BIBLIOTECA.Models
{
    public class Trazabilidad
    {

    }
    public class TrazabilidadResumen
    {
        public int TotalRegistros { get; set; }
        public TrazabilidadDetalle UltimoRegistro { get; set; }
    }

    public class TrazabilidadDetalle
    {
        public int IdTrazabilidad { get; set; }
        public int IdPrestamo { get; set; }
        public string TipoOperacion { get; set; }
        public string Detalles { get; set; }
        public DateTime FechaOperacion { get; set; }
        public int IdLibro { get; set; }
        public int IdUsuario { get; set; }
        public string LibroTitulo { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
    }

   
}
