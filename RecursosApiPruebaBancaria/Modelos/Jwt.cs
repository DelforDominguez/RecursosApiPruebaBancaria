using RecursosApiPruebaBancaria.ModelosTablas;
using RecursosApiPruebaBancaria.Recursos;
using System.Data;
using System.Security.Claims;

namespace RecursosApiPruebaBancaria.Modelos
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static dynamic validarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si esta enviando un Token",
                        result =""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;

                DataTable dt = DBDatos.Listar("pa_listarUsuarioSistema");
                List<DataRow> list = dt.AsEnumerable().ToList();

                List<MQUSUARIOSISTEMA15> usuario = new List<MQUSUARIOSISTEMA15>();
                usuario = (from DataRow row in dt.Rows

                           select new MQUSUARIOSISTEMA15
                           {
                               Id15 = Convert.ToInt32(row["Id15"].ToString()),
                               Id12 = Convert.ToInt32(row["Id12"].ToString()),
                               Usuario15 = row["Usuario15"].ToString(),
                               Password15 = row["Password15"].ToString(),
                               Estado15 = Convert.ToInt32(row["Estado15"].ToString())
                           }).ToList();

                MQUSUARIOSISTEMA15 usuario2 = usuario.FirstOrDefault(x => x.Id15 == Convert.ToInt32(id));

                return new
                {
                    success = true,
                    message = "Exito",
                    result = usuario2
                };
            }
            catch (Exception ex){
                return new
                {
                    success = false,
                    message = "Catch: " + ex.Message,
                    result = ""
                };
            }

        }

    }

}
