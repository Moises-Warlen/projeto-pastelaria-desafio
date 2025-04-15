using System.Collections.Generic;
using Pastelaria.Domain.Tarefa.Dto;

namespace Pastelaria.Domain.Tarefa
{
    // Define a interface para o repositório de tarefas
    public interface ITarefaRepository
    {
        // Obtém todas as tarefas
        IEnumerable<TarefaDto> GetAll();

        // Obtém uma tarefa específica pelo seu ID
        TarefaDto Get(int id);

        // Adiciona uma nova tarefa e retorna o ID gerado
        int Post(TarefaDto tarefa);

        // Atualiza uma tarefa existente para desativá-la e retorna a tarefa atualizada
        TarefaDto PutDesativarTarefa(int IdTarefa);

        // Remove uma tarefa pelo seu ID
        void Delete(int id);
    }
}
