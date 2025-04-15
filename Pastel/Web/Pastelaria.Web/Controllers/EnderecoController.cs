using Pastelaria.Web.Infra;  
using System.Web.Mvc;  
using Pastelaria.Web.Application.Endereco;  
using Pastelaria.Web.Application.Endereco.Model;  

namespace Pastelaria.Web.Controllers
{
    // Define que o controlador requer autenticação
    [Authorize]
    public class EnderecoController : BaseController
    {
        // Declaração da variável para a aplicação de Endereço
        private readonly EnderecoApplication _enderecoApplication;

        // Construtor que recebe uma instância da aplicação de Endereço
        public EnderecoController(EnderecoApplication enderecoApplication)
        {
            _enderecoApplication = enderecoApplication;
        }

        // Ação HTTP POST que recebe um modelo de Endereço e retorna uma view parcial
        [HttpPost]
        public ActionResult LinhaGrid(EnderecoModel endereco)
        {
            // Retorna a view parcial "_LinhaGridEndereco" com o modelo de Endereço fornecido
            return View("_LinhaGridEndereco", endereco);
        }

        // Ação para deletar um endereço pelo ID fornecido
        public ActionResult Deletar(int id)
        {
            // Chama o método de deletar da aplicação de Endereço
            _enderecoApplication.Delete(id);

            // Retorna um resultado de sucesso (presumivelmente definido em BaseController)
            return Success();
        }
    }
}
