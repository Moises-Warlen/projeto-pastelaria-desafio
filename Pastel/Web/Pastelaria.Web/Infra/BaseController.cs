using Newtonsoft.Json; // Biblioteca para manipulação de JSON
using System.Collections.Generic; // Biblioteca para coleções genéricas
using System.Net; // Biblioteca para códigos de status HTTP
using System.Web.Mvc; // Biblioteca para o padrão MVC na ASP.NET

namespace Pastelaria.Web.Infra
{
    // Classe base para todos os controllers da aplicação
    public class BaseController : Controller
    {
        // Método protegido que retorna um resultado com um código de status HTTP e um valor opcional
        protected ActionResult Content(HttpStatusCode status, string value = "")
        {
            Response.StatusCode = (int)status; // Define o código de status HTTP da resposta
            Response.TrySkipIisCustomErrors = true; // Ignora erros personalizados do IIS
            return Content(value); // Retorna o conteúdo com o valor fornecido
        }

        // Método para retornar uma resposta de erro com uma mensagem
        protected ActionResult Error(string message) => Content(HttpStatusCode.BadRequest, message);

        // Método para retornar uma resposta de erro com uma lista de mensagens
        protected ActionResult Error(IEnumerable<string> messages) => Content(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(messages));

        // Método para retornar uma resposta com o status HTTP 406 (Não Aceitável) e uma mensagem
        protected ActionResult NotAcceptable(string message) => Content(HttpStatusCode.NotAcceptable, message);

        // Método para retornar uma resposta com o status HTTP 200 (OK) sem conteúdo
        protected ActionResult Success() => Content(HttpStatusCode.OK);

        // Método para retornar uma resposta com o status HTTP 200 (OK) e uma mensagem
        protected ActionResult Success(string message) => Content(HttpStatusCode.OK, message);

        // Método para retornar uma resposta com o status HTTP 200 (OK) e conteúdo JSON
        protected ActionResult Success<T>(T content) => content == null ? Success() : Json(content, JsonRequestBehavior.AllowGet);

        // Método que é executado antes da ação do controller
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext); // Chama o método base

            // Adiciona dados da sessão ao ViewBag para que possam ser usados nas views
            ViewBag.Username = Session["Username"]; // Nome do usuário
            ViewBag.Email = Session["Email"]; // E-mail do usuário
            ViewBag.Id = Session["Id"]; // ID do usuário
            ViewBag.IdPerfil = Session["IdPerfil"]; // ID do perfil do usuário
        }
    }
}
