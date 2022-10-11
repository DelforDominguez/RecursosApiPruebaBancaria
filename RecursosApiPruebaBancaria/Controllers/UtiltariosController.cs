using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecursosApiPruebaBancaria.Modelos;
using RecursosApiPruebaBancaria.ModelosTablas;
using RecursosApiPruebaBancaria.Recursos;
using System.Data;

namespace RecursosApiPruebaBancaria.Controllers
{
    [ApiController]
    [Route("Utilitarios")]
    [Authorize]
    public class UtiltariosController : ControllerBase
    {
        [HttpGet]
        [Route("ListarTipoCalculo")]
        public dynamic ListarTipoCalculo()
        {
            DataTable dt = DBDatos.Listar("pa_ListarTipoCalculo");
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                result = new
                {
                    Result = JsonConvert.DeserializeObject<List<MQTIPOCALCULO02>>(json)
                }
            };
        }
        [HttpGet]
        [Route("ListarRoles")]
        public dynamic ListarRoles()
        {
            DataTable dt = DBDatos.Listar("pa_ListarRoles");
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                result = new
                {
                    Result = JsonConvert.DeserializeObject<List<MQTIPOROLES09>>(json)
                }
            };
        }

        [HttpGet]
        [Route("ListarRolesUsuario")]
        public dynamic ListarRolesUsuario()
        {
            DataTable dt = DBDatos.Listar("pa_ListarRolesUsuario");
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                result = new
                {
                    Result = JsonConvert.DeserializeObject<List<MDROLPERSONA13>>(json)
                }
            };
        }

        //MQTIPORELACION08
        [HttpGet]
        [Route("ListarTipoRelacion")]
        public dynamic ListarTipoRelacion()
        {
            DataTable dt = DBDatos.Listar("pa_ListarTipoRelacion");
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                result = new
                {
                    Result = JsonConvert.DeserializeObject<List<MQTIPORELACION08>>(json)
                }
            };
        }

        //ListarPersonalPorRol
        [HttpGet]
        [Route("ListarPersonalPorRol")]
        public dynamic ListarPersonalPorRol(int Id09, int Id08)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro("@iId09", Convert.ToString(Id09)),
                new Parametro("@iId08", Convert.ToString(Id08)),
            };
            DataTable dt = DBDatos.Listar("pa_ListarPersonalPorRol");
            string json = JsonConvert.SerializeObject(dt);
            return new
            {
                success = true,
                message = "exito",
                result = new
                {
                    Result = JsonConvert.DeserializeObject<List<ListaPersonalPorRol>>(json)
                }
            };
        }
    }
}
