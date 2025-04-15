using System.Web.Mvc;

namespace Pastelaria.Web.Filters
{
    // Define um filtro de ação que herda de ActionFilterAttribute
    public class SessionDataFilter : ActionFilterAttribute
    {
        // Método que é executado antes da ação do controlador ser chamada
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Recupera os dados da sessão do HttpContext
            var username = filterContext.HttpContext.Session["Username"] as string;
            var email = filterContext.HttpContext.Session["Email"] as string;
            var id = filterContext.HttpContext.Session["Id"] as int?;
            var idPerfil = filterContext.HttpContext.Session["IdPerfil"] as int?;

            // Atribui os valores recuperados à ViewBag para serem utilizados nas views
            filterContext.Controller.ViewBag.Username = username;
            filterContext.Controller.ViewBag.Email = email;
            filterContext.Controller.ViewBag.Id = id;
            filterContext.Controller.ViewBag.IdPerfil = idPerfil;

            // Chama o método base para garantir que qualquer lógica adicional na classe base também seja executada
            base.OnActionExecuting(filterContext);
        }
    }
}
