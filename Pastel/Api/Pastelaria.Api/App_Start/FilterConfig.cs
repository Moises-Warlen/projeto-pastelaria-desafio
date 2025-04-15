using System.Web; // Importa namespaces necessários para trabalhar com funcionalidades da web no ASP.NET
using System.Web.Mvc; // Importa namespaces relacionados ao ASP.NET MVC, incluindo filtros

namespace Pastelaria.Api
{
    // Classe responsável por configurar os filtros globais da aplicação
    public class FilterConfig
    {
        // Método que registra os filtros globais
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // Adiciona o filtro HandleErrorAttribute, que trata exceções não capturadas
            // e exibe uma página de erro personalizada (em vez de mostrar detalhes da exceção)
            filters.Add(new HandleErrorAttribute());
        }
    }
}
