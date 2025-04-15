using System.Web.Mvc; 
using System.Web.Routing; 

namespace Pastelaria.Api
{
    // Classe responsável pela configuração de rotas do aplicativo.
    public class RouteConfig
    {
        // Método estático que registra as rotas definidas no aplicativo.
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignora pedidos a recursos que terminem com ".axd", que são usados pelo sistema para fins de rastreamento e não precisam ser processados como rotas normais.
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Define uma rota padrão para o aplicativo.
            routes.MapRoute(
                name: "Default", // Nome da rota.
                url: "{controller}/{action}/{id}", // Padrão de URL, onde {controller} e {action} são placeholders para o controlador e a ação, e {id} é um parâmetro opcional.
                defaults: new { action = "Index", id = UrlParameter.Optional } // Define valores padrão para a rota, onde a ação padrão é "Index" e o parâmetro "id" é opcional.
            );
        }
    }
}
