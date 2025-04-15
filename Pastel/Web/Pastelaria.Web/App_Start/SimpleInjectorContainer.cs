using Pastelaria.Web.Application.Teste;
using SimpleInjector;

namespace Pastelaria.Web
{
    public static class SimpleInjectorContainer
    {
        public static Container RegisterServices()
        {
            // Cria uma nova instância do container Simple Injector.
            var container = new Container();

            // Registra o serviço TesteApplication no container.
            // Isso faz com que o Simple Injector saiba como criar uma instância de TesteApplication
            // quando ele for solicitado. Por padrão, o Simple Injector cria uma nova instância toda vez que o serviço é solicitado.
            container.Register<TesteApplication>();

            // Verifica se a configuração do container está correta.
            // O método Verify() lança uma exceção se houver algum problema com a configuração,
            // como dependências ausentes ou incorretas.
            container.Verify();

            // Retorna o container configurado.
            return container;
        }
    }
}
