using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reportes.Negocio
{
   
    public class Cliente
    {

        public string id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public long telefono { get; set; }
        public string rut { get; set; }
        public string fechaNacimiento { get; set; }
        public Direccion direccion { get; set; }
        public int activo { get; set; }
    }
}
