using Pastelaria.Api.Infra.Configuration; // Importa a configuração da infraestrutura da API
using System.Web.Http; // Importa o namespace para o ASP.NET Web API
using Pastelaria.Domain.Perfil; // Importa o namespace para o domínio de Perfil

namespace Pastelaria.Api.Controllers
{
    [RoutePrefix("api/perfil")] // Define o prefixo da rota para o controlador como "api/perfil"
    public class PerfilController : AuthApiController
    {
        // Declaração do repositório de perfis que será utilizado para acessar os dados
        private readonly IPerfilRepository _perfilRepository;

        // Construtor que recebe uma instância de IPerfilRepository para injeção de dependência
        public PerfilController(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        // Método GET para obter todos os perfis
        [HttpGet, Route("todos")] // Define a rota para o método como "api/perfil/todos"
        public IHttpActionResult GetPerfil()
        {
            var perfil = _perfilRepository.Get(); // Chama o método Get do repositório para obter os perfis
            return Ok(perfil); // Retorna os perfis com status HTTP 200 (OK)
        }
    }
}
