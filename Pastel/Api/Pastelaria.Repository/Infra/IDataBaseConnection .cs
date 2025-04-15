using System.Data;
using System.Data.SqlClient;

namespace Pastelaria.Repository.Infra
{
    // Interface que define a conexão com o banco de dados
    public interface IDatabaseConnection
    {
        // Propriedade que retorna a conexão SQL
        SqlConnection SqlConnection { get; }

        // Propriedade que retorna a transação SQL
        SqlTransaction SqlTransaction { get; }

        // Método para abrir uma transação sem especificar o nível de isolamento
        void OpenTransaction();

        // Método para abrir uma transação especificando o nível de isolamento
        void OpenTransaction(IsolationLevel isolationLevel);

        // Método para confirmar (commit) as operações realizadas na transação
        void Commit();

        // Método para desfazer (rollback) as operações realizadas na transação
        void Rollback();

        // Método para liberar os recursos utilizados pela conexão
        void Dispose();
    }
}
