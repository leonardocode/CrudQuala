using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class MonedaBL
    {
        MonedaDAL monedaDAL = new MonedaDAL();
        public List<Moneda> Listar()
        {
            return monedaDAL.Listar();

        }
    }
}
