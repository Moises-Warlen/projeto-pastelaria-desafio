using Pastelaria.Web.Filters; 
using System.Web.Mvc; 

namespace Pastelaria.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Adiciona o HandleErrorAttribute à coleção global de filtros
            // Este atributo lida com exceções e renderiza uma página de erro em caso de exceções não tratadas
            filters.Add(new HandleErrorAttribute());

            // Adiciona um filtro personalizado SessionDataFilter à coleção global de filtros
            // Este filtro personalizado seria responsável por lidar com tarefas relacionadas à sessão
            // (Certifique-se de que SessionDataFilter está implementado no namespace Pastelaria.Web.Filters)
            filters.Add(new SessionDataFilter()); // Adicione esta linha
        }
    }
}
