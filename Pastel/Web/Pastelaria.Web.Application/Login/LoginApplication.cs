// Importa namespaces necessários para a aplicação
using Pastelaria.Web.Application.Infra;
using Pastelaria.Web.Application.Infra.Extensions;

namespace Pastelaria.Web.Application.Login.Model
{
    // Define a classe LoginApplication que herda de BaseApplication
    public class LoginApplication : BaseApplication
    {
        // Construtor da classe LoginApplication
        // Passa a URL da API para o construtor da classe base
        public LoginApplication() : base($"http://localhost:51169/api/endereco")
        {
        }

        // Método para fazer uma requisição POST com um objeto LoginModel
        // Retorna um objeto do tipo Response
        public Response Post(LoginModel login)
        {
            // Envia a requisição POST com o modelo de login e retorna a resposta formatada
            return Request.Post(login).AsResponse();
        }
    }
}
