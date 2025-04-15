using Pastelaria.Web.Infra; 
using System.Web.Mvc; 
namespace Pastelaria.Web.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View(); // Retorna a view associada ao método Index(), normalmente representando a página de login.
        }
    }
}
