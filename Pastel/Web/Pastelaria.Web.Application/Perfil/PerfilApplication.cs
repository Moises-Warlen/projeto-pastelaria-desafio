using System.Collections.Generic;
using Pastelaria.Web.Application.Infra;
using Pastelaria.Web.Application.Infra.Extensions;
using Pastelaria.Web.Application.Perfil.Model;


namespace Pastelaria.Web.Application.Perfil
{
    // Classe PerfilApplication que herda de BaseApplication
    public class PerfilApplication : BaseApplication
    {
        // Construtor da classe PerfilApplication
        public PerfilApplication() : base($"http://localhost:51169/api/perfil")
        {
            // O construtor chama o construtor da classe base com a URL base da API
        }

        // Método para obter a lista de perfis
        public Response<IEnumerable<PerfilModel>> GetPerfil()
        {
            // Faz uma requisição GET para o endpoint "todos" e converte a resposta para um tipo Response que contém uma lista de PerfilModel
            return Request.Get("todos").AsResponse<IEnumerable<PerfilModel>>();
        }
    }
}
