using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class SucursalBL
    {
        SucursalDAL sucursalDAL = new SucursalDAL();
        public List<Sucursal> Listar()
        {
            return sucursalDAL.Listar();
        }

        public Sucursal obtenerSucursalPorCodigo(int Codigo)
        {
            return sucursalDAL.obtenerSucursalPorCodigo(Codigo);
        }

        public int eliminarPorCodigo(int Codigo)
        {
            return sucursalDAL.eliminarPorCodigo(Codigo);
        }

        public int Editar(Sucursal sucursal)
        {
            DateTime fechaCreacion;
            if (TryConvertStringToDateTime(sucursal.fechaCreacion, out fechaCreacion))
            {
                sucursal.fechaCreacion = fechaCreacion.ToString();

                return sucursalDAL.Editar(sucursal);
            }
            else
            {
                throw new FormatException("Fecha inválida, el formato debe ser 'dd/MM/yyyy'.");
            }
            
        }

        public int guardar(Sucursal sucursal)
        {
            DateTime fechaCreacion;
            if (TryConvertStringToDateTime(sucursal.fechaCreacion, out fechaCreacion))
            {
                sucursal.fechaCreacion = fechaCreacion.ToString();

                return sucursalDAL.guardar(sucursal);
            }
            else
            {
                throw new FormatException("Fecha inválida, el formato debe ser 'dd/MM/yyyy'.");
            }
            
        }

        private bool TryConvertStringToDateTime(string fechaCreacion, out DateTime fecha)
        {
            return DateTime.TryParseExact(fechaCreacion, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha);
        }
    }
}
