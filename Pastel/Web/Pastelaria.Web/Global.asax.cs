using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pastelaria.Web
{
    // Classe que herda de System.Web.HttpApplication, a base para a configuração do aplicativo ASP.NET MVC
    public class MvcApplication : System.Web.HttpApplication
    {
        // Método que é chamado quando a aplicação é iniciada
        protected void Application_Start()
        {
            // Configura o resolvedor de dependências do Simple Injector para o MVC
            // O método SimpleInjectorContainer.RegisterServices() configura e retorna o contêiner de dependências
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(SimpleInjectorContainer.RegisterServices()));

            // Registra todas as áreas do MVC (útil se o projeto utiliza áreas para organização)
            AreaRegistration.RegisterAllAreas();

            // Registra os filtros globais que serão aplicados a todas as requisições do MVC
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Configura as rotas do MVC para que as requisições sejam direcionadas aos controllers apropriados
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
