using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WORKER_GESTION_BIBLIOTECA.Models
{
    public class Usuario
    {
        public long idUsuario { get; set; }
        public string cedula { get; set; }
        public string nombre { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string correo { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasena { get; set; }
        public bool esEmpleado { get; set; }
        public char estado { get; set; }
        public DateTime fechaRegistro { get; set; }
    }
}
