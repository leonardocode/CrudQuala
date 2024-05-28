using CapaDatos;
using CapaEntidades;
using CapaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace WSCrudAsistQuala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        SucursalBL sucursalBL = new SucursalBL();
        


        [HttpGet]
        [Route("ConsultarSucursales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Sucursal>> ConsultarSucursales()
        {

            try
            {

                var result = sucursalBL.Listar();
                if (result == null || result.Count == 0)
                {
                    return NotFound("No se encontraron registros");
                }
                else
                {
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("obtenerSucursalPorCodigo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Sucursal> obtenerSucursalPorCodigo(int Codigo)
        {
            try
            {
                if(Codigo == null || Codigo <= 0)
                {
                    return BadRequest("El codigo de la sucursal es obligatorio");
                }

                var result = sucursalBL.obtenerSucursalPorCodigo(Codigo);
                if (result == null)
                {
                    return NotFound("No se encontraron registros");
                }
                else
                {
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Eliminar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> Eliminar(int Codigo)
        {
            if (Codigo == null || Codigo <= 0)
            {
                return BadRequest("El codigo de la sucursal es obligatorio");
            }

            try
            {

                var result = sucursalBL.eliminarPorCodigo(Codigo);
                if (result == null)
                {
                    return NotFound("Error: no se pudo eliminar el registro");
                }
                else
                {
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("Editar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> Editar(Sucursal sucursal)
        {
            if (sucursal.Codigo == null || sucursal.Codigo <= 0)
            {
                return BadRequest("El codigo de la sucursal es obligatorio");
            }

            if (sucursal.descripcion == null || sucursal.descripcion == "" || string.IsNullOrEmpty(sucursal.descripcion))
            {
                return BadRequest("La descripcion de la sucursal es obligatorio");
            }

            if (sucursal.direccion == null || sucursal.direccion == "" || string.IsNullOrEmpty(sucursal.direccion))
            {
                return BadRequest("La direccion de la sucursal es obligatorio");
            }

            if (sucursal.identificacion == null || sucursal.identificacion == "" || string.IsNullOrEmpty(sucursal.identificacion))
            {
                return BadRequest("La identificacion de la sucursal es obligatorio");
            }

            if (sucursal.fechaCreacion == null || sucursal.fechaCreacion == "" || string.IsNullOrEmpty(sucursal.fechaCreacion))
            {
                return BadRequest("La fecha de creacion de la sucursal es obligatorio");
            }

            if(Convert.ToDateTime(sucursal.fechaCreacion) < DateTime.Now)
            {
                return BadRequest("La fecha de creacion no puede ser menor a la fecha actual");
            }

            if (sucursal.idMoneda == null || sucursal.idMoneda <= 0)
            {
                return BadRequest("El codigo de la moneda es obligatorio");
            }

            try
            {

                var result = sucursalBL.Editar(sucursal);
                if (result == null)
                {
                    return NotFound("Error: no se pudo editar el registro");
                }
                else
                {
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> guardar(Sucursal sucursal)
        {
            DateTime fecha;

            if (sucursal.descripcion == null || sucursal.descripcion == "" || string.IsNullOrEmpty(sucursal.descripcion))
            {
                return BadRequest("La descripcion de la sucursal es obligatorio");
            }

            if (sucursal.direccion == null || sucursal.direccion == "" || string.IsNullOrEmpty(sucursal.direccion))
            {
                return BadRequest("La direccion de la sucursal es obligatorio");
            }

            if (sucursal.identificacion == null || sucursal.identificacion == "" || string.IsNullOrEmpty(sucursal.identificacion))
            {
                return BadRequest("La identificacion de la sucursal es obligatorio");
            }

            if (sucursal.fechaCreacion == null || sucursal.fechaCreacion == "" || string.IsNullOrEmpty(sucursal.fechaCreacion))
            {
                return BadRequest("La fecha de creacion de la sucursal es obligatorio");
            }

            if (Convert.ToDateTime(sucursal.fechaCreacion) < DateTime.Now)
            {
                return BadRequest("La fecha de creacion no puede ser menor a la fecha actual");
            }

            if (sucursal.idMoneda == null || sucursal.idMoneda <= 0)
            {
                return BadRequest("El codigo de la moneda es obligatorio");
            }

            

            try
            {

                var result = sucursalBL.guardar(sucursal);
                if (result == null || result == 0)
                {
                    return NotFound("Error: no se pudo guardar el registro");
                }
                else
                {
                    return Ok(result);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
