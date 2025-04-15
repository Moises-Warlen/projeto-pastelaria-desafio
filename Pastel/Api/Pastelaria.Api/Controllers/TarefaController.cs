using System.Web.Http; 
using Pastelaria.Api.Infra.Configuration; 
using Pastelaria.Domain.Tarefa; 
using Pastelaria.Domain.Tarefa.Dto; 
namespace Pastelaria.Api.Controllers
{
    // Controlador responsável por gerenciar operações relacionadas às tarefas da aplicação.
    [RoutePrefix("api/tarefa")] // Define um prefixo de rota para todos os endpoints deste controlador.
    public class TarefaController : AuthApiController
    {
        private readonly ITarefaRepository _tarefaRepository;

        // Construtor que recebe uma instância do repositório de tarefas para realizar operações de acesso a dados.
        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        // Endpoint para obter todas as tarefas cadastradas na aplicação.
        [HttpGet, Route("todas")]
        public IHttpActionResult GetTarefas()
        {
            var tarefas = _tarefaRepository.GetAll(); // Chama o método Get do repositório para obter todas as tarefas.
            return Ok(tarefas); // Retorna a lista de tarefas com status 200 OK.
        }

        // Endpoint para obter uma tarefa específica com base em seu ID.
        [HttpGet, Route("{idTarefa:int}")]
        public IHttpActionResult Get(int idTarefa)
        {
            var tarefa = _tarefaRepository.Get(idTarefa); // Obtém a tarefa com o ID especificado.
            if (tarefa == null)
            {
                return NotFound(); // Retorna 404 Not Found se a tarefa não for encontrada.
            }
            return Ok(tarefa); // Retorna a tarefa com status 200 OK.
        }

        // Endpoint para excluir uma tarefa com base em seu ID.
        [HttpDelete, Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            _tarefaRepository.Delete(id); // Chama o método Delete do repositório para remover a tarefa com o ID especificado.
            return Ok(); // Retorna status 200 OK indicando que a operação foi bem-sucedida.
        }

        // Endpoint para concluir uma tarefa com base em seu ID.
        [HttpPut, Route("concluir/{id}")]
        public IHttpActionResult PutConcluirTarefa(int id)
        {
            _tarefaRepository.PutDesativarTarefa(id); // Chama o método PutDesativarTarefa do repositório para marcar a tarefa como concluída.
            return Ok(); // Retorna status 200 OK indicando que a operação foi bem-sucedida.
        }

        // Endpoint para criar uma nova tarefa.
        [HttpPost]
        public IHttpActionResult Post(TarefaDto tarefa)
        {
            var Idtarefa = _tarefaRepository.Post(tarefa); // Adiciona uma nova tarefa e obtém o ID da tarefa criada.
            return Ok(); // Retorna status 200 OK indicando que a tarefa foi criada com sucesso.
        }
    }
}
