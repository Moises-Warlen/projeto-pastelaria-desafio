using Pastelaria.Domain.Endereco;
using Pastelaria.Domain.Perfil;
using Pastelaria.Domain.Tarefa;
using Pastelaria.Domain.Telefone;
using Pastelaria.Domain.Teste;
using Pastelaria.Domain.Usuario;
using Pastelaria.Repository;
using Pastelaria.Repository.Infra;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Pastelaria.Api
{
    // Classe responsável pela configuração do SimpleInjector
    public static class SimpleInjectorContainer
    {
        // Instância estática do Container do SimpleInjector
        private static readonly Container Container = new Container();

        // Método responsável por construir e configurar o Container
        public static Container Build()
        {
            // Define o estilo de vida (lifecycle) padrão como "Scoped" assíncrono
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Registro da conexão com o banco de dados com estilo de vida Scoped (instância única por request)
            Container.Register<IDatabaseConnection, DatabaseConnection>(Lifestyle.Scoped);

            // Exemplo de um possível registro comentado (opcional, dependendo das necessidades)
            //Container.Register<Notification>(Lifestyle.Scoped);

            // Registro de todos os repositórios
            RegisterRepositories();

            // Verificação do Container para garantir que todas as dependências estão resolvidas corretamente
            Container.Verify();
            return Container;
        }

        // Método privado responsável por registrar os repositórios
        private static void RegisterRepositories()
        {
            // Registro dos repositórios com as respectivas interfaces e implementações
            Container.Register<ITesteRepository, TesteRepository>();
            Container.Register<IUsuarioRepository, UsuarioRepository>();
            Container.Register<ITarefaRepository, TarefaRepository>();
            Container.Register<IEnderecoRepository, EnderecoRepository>();
            Container.Register<ITelefoneRepository, TelefoneRepository>();
            Container.Register<IPerfilRepository, PerfilRepository>();
        }
    }
}
