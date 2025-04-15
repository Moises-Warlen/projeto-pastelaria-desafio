using System.Web.Mvc;
using System.Web.Routing;

namespace Pastelaria.Web
{
    // Classe responsável pela configuração das rotas da aplicação ASP.NET MVC
    public class RouteConfig
    {
        // Método para registrar as rotas
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignorar requisições para arquivos de recursos como WebResource.axd e ScriptResource.axd
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Definição da rota padrão da aplicação
            routes.MapRoute(
                "Default", // Nome da rota
                "{controller}/{action}/{id}", // Padrão da URL
                new { controller = "Login", action = "Index", id = UrlParameter.Optional } // Valores padrão
            );
        }
    }
}
