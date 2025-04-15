using Pastelaria.Web.Application.Telefone; 
using Pastelaria.Web.Infra; 
using System.Web.Mvc; 
using Pastelaria.Web.Application.Telefone.Model; 

namespace Pastelaria.Web.Controllers
{
    // O atributo [Authorize] indica que este controlador requer autenticação
    [Authorize]
    public class TelefoneController : BaseController
    {
        private readonly TelefoneApplication _telefoneApplication;

        // O construtor do controlador recebe uma instância de TelefoneApplication
        // e a atribui ao campo privado _telefoneApplication
        public TelefoneController(TelefoneApplication telefoneApplication)
        {
            _telefoneApplication = telefoneApplication;
        }

        // O atributo [HttpPost] indica que este método só deve responder a requisições POST
        public ActionResult LinhaGrid(TelefoneModel telefone)
        {
            // Retorna a view parcial "_LinhaGridTelefone" passando o modelo de telefone
            return View("_LinhaGridTelefone", telefone);
        }

        // O método Deletar é chamado para deletar um telefone com o ID especificado
        public ActionResult Deletar(int id)
        {
            // Chama o método Delete da aplicação de telefone para remover o telefone com o ID fornecido
            _telefoneApplication.Delete(id);
            // Validar se a deleção foi bem-sucedida (a validação real não está mostrada aqui)
            return Success(); // Retorna um resultado de sucesso, presumivelmente uma resposta padrão de sucesso
        }
    }
}
