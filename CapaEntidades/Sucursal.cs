using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Sucursal
    {
        public int Codigo { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public string identificacion { get; set; }
        public string fechaCreacion { get; set; }
        public int idMoneda { get; set; }
        public string moneda { get; set; }
    }
}
