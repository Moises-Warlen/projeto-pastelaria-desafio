using Pastelaria.Domain.Telefone;
using System.Web.Http;
using Pastelaria.Api.Infra.Configuration;

namespace Pastelaria.Api.Controllers
{
    [RoutePrefix("api/telefone")] // Define o prefixo de rota para todas as rotas neste controlador como "api/telefone".
    public class TelefoneController : AuthApiController
    {
        private readonly ITelefoneRepository _telefoneRepository; // Declara uma variável privada para armazenar o repositório de telefone.

        // Construtor do controlador que recebe um repositório de telefone através da injeção de dependência.
        public TelefoneController(ITelefoneRepository telefoneRepository)
        {
            _telefoneRepository = telefoneRepository; // Inicializa o repositório de telefone.
        }

        // Endpoint para obter telefones de um usuário específico baseado no ID do usuário.
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var telefones = _telefoneRepository.Get(id); // Obtém a lista de telefones para o ID fornecido.
            return Ok(telefones); // Retorna a lista de telefones com um código de status HTTP 200 (OK).
        }

        // Endpoint para deletar um telefone baseado no ID do telefone.
        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _telefoneRepository.Delete(id); // Deleta o telefone com o ID fornecido.

            return Ok(); // Retorna um código de status HTTP 200 (OK) indicando que a operação foi bem-sucedida.
        }
    }
}
