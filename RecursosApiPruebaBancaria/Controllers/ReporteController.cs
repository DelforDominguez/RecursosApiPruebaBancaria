using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecursosApiPruebaBancaria.Modelos;
using RecursosApiPruebaBancaria.Recursos;
using System.Data;

namespace RecursosApiPruebaBancaria.Controllers
{
    [ApiController]
    [Route("Reportes")]
    [Authorize]
    public class ReporteController : ControllerBase
    {
        [HttpGet]
        [Route("ReporteMetaVentas")]
        public dynamic ReporteMetaVentas([FromHeader] ParamRepMetaVentas Datos)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iIdGerente", Convert.ToString(Datos.IdGerente)),
                new Parametro("@sFechaIni", Datos.FechaIni),
                new Parametro("@sFechaFin", Datos.FechaFin),
            };

            DataTable dt = DBDatos.Listar("pa_ListarReporteMetaVentas", parametros);
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                data = new
                {
                    ListarReporteMetaVentas = JsonConvert.DeserializeObject<List<ListarReporteMetaVentas>>(json)
                }
            };
        }
    }
}
