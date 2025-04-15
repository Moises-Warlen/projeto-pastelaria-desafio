using Pastelaria.Api.Infra.Configuration; 
using Pastelaria.Domain.Teste; 
using Pastelaria.Domain.Teste.Dto; 
using System.Web.Http; 

namespace Pastelaria.Api.Controllers
{
    [RoutePrefix("api/teste")] // Define o prefixo da rota para todos os métodos deste controlador.
    public class TesteController : AuthApiController // Define o controlador TesteController que herda de AuthApiController.
    {
        private readonly ITesteRepository _testeRepository; // Declara uma instância de ITesteRepository para acessar os dados.

        public TesteController(ITesteRepository testeRepository)
        {
            _testeRepository = testeRepository; // Inicializa o repositório de teste através da injeção de dependência.
        }

        [HttpGet, Route("todos")] // Define um endpoint HTTP GET com rota "api/teste/todos".
        public IHttpActionResult GetTestes()
        {
            return Ok(_testeRepository.Get()); // Retorna todos os testes obtidos do repositório.
        }


        [HttpPost, Route("")] // Define um endpoint HTTP POST com rota "api/teste".
        public IHttpActionResult Post(TesteDto teste)
        {
            _testeRepository.Post(teste); // Chama o método Post do repositório para adicionar um novo teste.

            return Ok(); // Retorna uma resposta HTTP 200 OK.
        }
    }
}
