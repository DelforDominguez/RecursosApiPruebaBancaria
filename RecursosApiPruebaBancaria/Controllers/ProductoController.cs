using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecursosApiPruebaBancaria.Modelos;
using RecursosApiPruebaBancaria.ModelosTablas;
using RecursosApiPruebaBancaria.Recursos;
using System.Data;
using System.Runtime.InteropServices;

namespace RecursosApiPruebaBancaria.Controllers
{
    [ApiController]
    [Route("Producto")]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        [HttpGet]
        [Route("Listar")]
        public dynamic ListarProducto(string iIdProducto ="0", string sDescripcionProducto="Null") 
        {
            if (sDescripcionProducto == "Null") {
                sDescripcionProducto = "";
            };

            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId01",iIdProducto),
                new Parametro("@sDescription", sDescripcionProducto)
            };

            DataTable tProductos = DBDatos.Listar("pa_ListarProductos", parametros);

            string jsonProductos = JsonConvert.SerializeObject(tProductos);

            return new
            {
                success = true,
                message = "exito",
                result = new
                {
                    Productos = JsonConvert.DeserializeObject<List<ListaProducto>>(jsonProductos)
                }
            };
        }

        [HttpPost]
        [Route("Modificar")]
        public dynamic ModificarProducto([FromBody] MQMAPRODUCTOS01 Datos){
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId01", Convert.ToString(Datos.Id01)),
                new Parametro("@sCatalogo01", Datos.Catalogo01),
                new Parametro("@sDescripcion01", Datos.Descripcion01),
                new Parametro("@iTipoCalculo02", Convert.ToString(Datos.TipoCalculo02)),
                new Parametro("@iPuntos01", Convert.ToString(Datos.Puntos01)),
                new Parametro("@dPorcentaje01", Convert.ToString(Datos.Porcentaje01)),
                new Parametro("@iEstado01", Convert.ToString(Datos.Estado01))
            };

            DBDatos.Ejecutar("pa_ActualizarProductos", parametros);

            return new
            {
                success = true,
                message = "exito",
                result = "Operacion Realizada con exito"
            };
        }
    }
}
