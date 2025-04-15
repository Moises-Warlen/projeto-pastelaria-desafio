using Pastelaria.Web.Infra; 
using System.Web.Mvc; 

namespace Pastelaria.Web.Controllers
{
    // A classe HomeController está decorada com o atributo [Authorize]
    // Isso significa que o acesso aos métodos desta classe requer autenticação
    [Authorize]
    public class HomeController : BaseController
    {
        // GET: Home
        // Este método de ação é responsável por lidar com requisições HTTP GET para a rota "Home"
        // Ele retorna a view padrão para esta ação
        public ActionResult Index()
        {
            // O método View() renderiza a view associada a este método de ação
            return View();
        }
    }
}
