using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecursosApiPruebaBancaria.Modelos;
using RecursosApiPruebaBancaria.ModelosTablas;
using RecursosApiPruebaBancaria.Recursos;
using System.Data;

namespace RecursosApiPruebaBancaria.Controllers
{
    [ApiController]
    [Route("Ventas")]
    [Authorize]
    public class VentasController : ControllerBase
    {
        [HttpGet]
        [Route("ListarVentasCAB")]
        public dynamic ListarVentasCAB(string iIdVenta25 = "0") 
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId25",iIdVenta25),
            };

            DataTable dt = DBDatos.Listar("pa_ListarVentasCAB", parametros);
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                data = new
                {
                    VentasCAB = JsonConvert.DeserializeObject<List<VentasCAB>>(json)
                }
            };
        }

        [HttpGet]
        [Route("ListarVentasDET")]
        public dynamic ListarVentasDET(string iIdVenta25 = "0")
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId25",iIdVenta25),
            };

            DataTable dt = DBDatos.Listar("pa_ListarVentasDET", parametros);
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                data = new
                {
                    VentasDET = JsonConvert.DeserializeObject<List<VentasDET>>(json)
                }
            };
        }

        [HttpPost]
        [Route("ModificarCAB")]
        public dynamic ModificarCAB([FromBody] RegVentasCab Datos)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId25", Convert.ToString(Datos.Id25)),
                new Parametro("@sFechaEmi25", Datos.FechaEmi25),
                new Parametro("@iIdVendedor12", Convert.ToString(Datos.IdVendedor12)),
                new Parametro("@iIdCliente12", Convert.ToString(Datos.IdCliente12)),
            };
            string Result = "";
            Result=DBDatos.EjecutarDato("pa_ActualizarRegVentas", parametros);

            return new
            {
                success = true,
                message = "exito",
                data = Result
            };
        }

        [HttpPost]
        [Route("ModificarDET")]
        public dynamic ModificarDET([FromBody] RegVentasDet Datos)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId25", Convert.ToString(Datos.Id25)),
                new Parametro("@iId27", Convert.ToString(Datos.Id27)),
                new Parametro("@iIdProducto01", Convert.ToString(Datos.IdProducto01)),
                new Parametro("@iCantidad27", Convert.ToString(Datos.Cantidad27)),
                new Parametro("@dMontoDesembolsado27", Convert.ToString(Datos.MontoDesembolsado27)),
            };

            string Result = "";
            Result = DBDatos.EjecutarDato("pa_ActulizarRegVentasDET", parametros);

            return new
            {
                success = true,
                message = "Operacion Realizada con exito",
                data = Result
            };
        }

        [HttpDelete]
        [Route("EliminarDET")]
        public dynamic EliminarDET(int id25, int id27) {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId25", Convert.ToString(id25)),
                new Parametro("@iId27", Convert.ToString(id27)),
            };

            DBDatos.Ejecutar("pa_EliminarRegVentasDET", parametros);

            return new
            {
                success = true,
                message = "Operacion Realizada con exito",
                data = ""
            };
            //
        }

    }
}
