using Pastelaria.Api.Infra.Configuration;  
using Pastelaria.Domain.Endereco;          
using System.Web.Http;                     

namespace Pastelaria.Api.Controllers
{
    [RoutePrefix("api/endereco")]  // Define um prefixo para todas as rotas deste controlador
    public class EnderecoController : AuthApiController  // Controlador que herda de AuthApiController, o que pode incluir autenticação e autorização
    {
        private readonly IEnderecoRepository _enderecoRepository; // Declara uma variável privada para o repositório de endereços

        public EnderecoController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;  // Injeta o repositório de endereços via construtor
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var enderecos = _enderecoRepository.Get(id);  // Obtém o endereço correspondente ao id fornecido
            return Ok(enderecos);  // Retorna o resultado com status HTTP 200 (OK)
        }

        [HttpDelete, Route("{id}")]  // Define a rota de deleção como "api/endereco/{id}"
        public IHttpActionResult Delete(int id)
        {
            _enderecoRepository.Delete(id);  // Exclui o endereço correspondente ao id fornecido
            return Ok();  // Retorna uma resposta de sucesso HTTP 200 (OK)
        }
    }
}
