using Pastelaria.Repository.Infra;
using System.Collections.Generic;
using System.Linq;
using Pastelaria.Domain.Usuario.Dto;
using Pastelaria.Domain.Tarefa;
using Pastelaria.Repository.Infra.Extensions;
using System;
using Pastelaria.Domain.Tarefa.Dto;

namespace Pastelaria.Repository
{
    public class TarefaRepository : BaseRepository, ITarefaRepository
    {
        public TarefaRepository(IDatabaseConnection connection) : base(connection)
        {
            // Construtor da classe TarefaRepository. Recebe uma conexão com o banco de dados e a passa para a classe base.
        }

        // Enumeração dos procedimentos armazenados utilizados nesta classe.
        private enum Procedures
        {
            ListarTarefasAtivas,       // Procedimento para listar todas as tarefas ativas
            AtualizarConcluirTarefa,   // Procedimento para atualizar ou concluir uma tarefa
            DeletarTarefa,             // Procedimento para deletar uma tarefa
            ConcluirTarefaUsuario,     // Procedimento para concluir uma tarefa atribuída a um usuário
            AdicionarTarefa,           // Procedimento para adicionar uma nova tarefa
            BuscarTarefaPorId          // Procedimento para buscar uma tarefa pelo ID
        }

        // Obtém todas as tarefas ativas.
        public IEnumerable<TarefaDto> GetAll()
        {
            ExecuteProcedure(Procedures.ListarTarefasAtivas);  // Executa o procedimento para listar todas as tarefas ativas
            using (var r = ExecuteReader())             // Utiliza um leitor para obter os resultados
                return r.CastEnumerable<TarefaDto>();   // Converte os resultados para IEnumerable<TarefaDto>
        }

        // Obtém uma tarefa específica pelo ID.
        public TarefaDto Get(int id)
        {
            ExecuteProcedure(Procedures.BuscarTarefaPorId);  // Executa o procedimento para buscar uma tarefa por ID
            AddParameter("@IdTarefa", id);                    // Adiciona o parâmetro ID ao procedimento
            using (var r = ExecuteReader())
                return r.Read() ? r.Cast<TarefaDto>() : null; // Retorna a tarefa encontrada ou null se não encontrada
        }

        // Deleta uma tarefa específica pelo ID.
        public void Delete(int id)
        {
            ExecuteProcedure(Procedures.DeletarTarefa);  // Executa o procedimento para deletar uma tarefa
            AddParameter("@IdTarefa", id);                // Adiciona o parâmetro ID ao procedimento
            ExecuteNonQuery();                          // Executa o comando não-query para deletar a tarefa
        }

        // Desativa uma tarefa específica pelo ID.
        public TarefaDto PutDesativarTarefa(int IdTarefa)
        {
            ExecuteProcedure(Procedures.ConcluirTarefaUsuario);  // Executa o procedimento para desativar uma tarefa atribuída a um usuário
            AddParameter("@IdTarefa", IdTarefa);                  // Adiciona o parâmetro ID ao procedimento
            using (var r = ExecuteReader())
                return r.Read() ? r.Cast<TarefaDto>() : null;     // Retorna a tarefa desativada ou null se não encontrada
        }

        // Adiciona uma nova tarefa e retorna o ID gerado para a nova tarefa.
        public int Post(TarefaDto tarefa)
        {
            ExecuteProcedure(Procedures.AdicionarTarefa); // Executa o procedimento para adicionar uma nova tarefa

            AddParameter("@IdUsuario", tarefa.IdUsuario); // Adiciona os parâmetros necessários ao procedimento
            AddParameter("@Descricao", tarefa.Descricao);
            AddParameter("@CriadorId", tarefa.CriadorId);
            return ExecuteNonQueryWithReturn<int>(); // Executa o comando não-query e retorna o ID gerado
        }
    }
}
