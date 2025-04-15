using Pastelaria.Api.Infra.Configuration;
using System.Web.Http;

namespace Pastelaria.Api.Controllers
{
    // Classe PingController que herda de AuthApiController
    public class PingController : AuthApiController
    {
        // Método Get que responde a requisições HTTP GET
        public IHttpActionResult Get() => Ok(); // Retorna uma resposta HTTP 200 OK
    }
}
