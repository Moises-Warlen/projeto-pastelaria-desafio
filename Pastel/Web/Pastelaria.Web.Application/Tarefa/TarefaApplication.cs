using Pastelaria.Web.Application.Infra;
using Pastelaria.Web.Application.Infra.Extensions;
using Pastelaria.Web.Application.Tarefa.Model;
using System.Collections.Generic;

namespace Pastelaria.Web.Application.Tarefa
{// Classe TarefaApplication herda de BaseApplication
    public class TarefaApplication : BaseApplication
    {
        // Declaração de uma variável privada para armazenar uma instância de UsuarioApplication
        private readonly UsuarioApplication _usuarioApplication;

        // Construtor da classe TarefaApplication
        // Inicializa a base da aplicação com a URL da API de tarefas
        public TarefaApplication() : base($"http://localhost:51169/api/tarefa")
        {
        }

        // Método para enviar uma requisição POST para criar uma nova tarefa
        // Recebe um objeto TarefaModel e retorna um objeto Response
        public Response Post(TarefaModel tarefa)
        {
            return Request.Post(tarefa).AsResponse();
        }

        // Método para enviar uma requisição PUT para marcar uma tarefa como concluída
        // Recebe o ID da tarefa a ser concluída e retorna um objeto Response
        public Response PutConcluirTarefa(int id)
        {
            return Request.Put($"concluir/{id}").AsResponse();
        }

        // Método para enviar uma requisição DELETE para remover uma tarefa
        // Recebe o ID da tarefa a ser removida e retorna um objeto Response
        public Response Delete(int id)
        {
            return Request.Delete(id).AsResponse();
        }

        // Método para enviar uma requisição GET para obter todas as tarefas
        // Retorna um objeto Response contendo uma coleção de TarefaModel
        public Response<IEnumerable<TarefaModel>> GetTarefas()
            => Request.Get("todas").AsResponse<IEnumerable<TarefaModel>>();

        // Método para enviar uma requisição GET para obter uma tarefa específica pelo ID
        // Recebe o ID da tarefa e retorna um objeto Response contendo um TarefaModel
        public Response<TarefaModel> GetTarefa(int id)
            => Request.Get(new { id }).AsResponse<TarefaModel>();
    }

}

