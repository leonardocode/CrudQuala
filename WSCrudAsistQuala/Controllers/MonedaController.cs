using CapaEntidades;
using CapaNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WSCrudAsistQuala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaController : ControllerBase
    {
        MonedaBL monedaBL = new MonedaBL();


        [HttpGet]
        [Route("ConsultarMonedas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Moneda>> ConsultarMonedas()
        {

            try
            {

                var result = monedaBL.Listar();
                if(result == null || result.Count == 0)
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

    }
}
