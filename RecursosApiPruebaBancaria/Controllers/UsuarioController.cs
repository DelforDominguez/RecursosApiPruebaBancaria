using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RecursosApiPruebaBancaria.Modelos;
using RecursosApiPruebaBancaria.ModelosTablas;
using RecursosApiPruebaBancaria.Recursos;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RecursosApiPruebaBancaria.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : ControllerBase
    {
        public IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public dynamic IniciarSesion([FromBody] user DatosIngreso)
        {
            //var data = JsonConvert.DeserializeObject<user>(DatosIngreso.ToString());

            string user = DatosIngreso.usuario.ToString();
            string password = DatosIngreso.password.ToString();
           
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
            MQUSUARIOSISTEMA15 usuarioValida = usuario.FirstOrDefault(x => x.Usuario15 == user && x.Password15==password);



            if (usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales no Validas",
                    data=""
                };
            }

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id",usuarioValida.Usuario15),
                new Claim("usuario",usuarioValida.Password15)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: singIn
                );

            return new
            {
                success = true,
                message = "Exitos",
                data = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
