using System;
using System.Data;
using System.Data.SqlClient;

namespace Pastelaria.Repository.Infra
{
    public class DatabaseConnection : IDatabaseConnection, IDisposable
    {
        // Construtor da classe DatabaseConnection
        public DatabaseConnection()
        {
            // Configuração da conexão com o banco de dados SQL Server
            SqlConnection = new SqlConnection(
                "Data Source=DESKTOP-NK5319M;" + // Nome do servidor
                "Initial Catalog=Pastelaria;" + // Nome da base de dados
                "Integrated Security=True;" + // Usar a autenticação do Windows
                "Connection Timeout=300" // Tempo limite para conexão (em segundos)
            );
        }

        public SqlConnection SqlConnection { get; } // Objeto para gerenciar a conexão com o banco de dados
        public SqlTransaction SqlTransaction { get; set; } // Objeto para gerenciar a transação SQL

        // Método privado para abrir a conexão com o banco de dados
        private void OpenConnection()
        {
            // Verifica se a conexão está quebrada; se sim, fecha e reabre
            if (SqlConnection.State == ConnectionState.Broken)
            {
                SqlConnection.Close(); // Fecha a conexão quebrada
                SqlConnection.Open(); // Reabre a conexão
            }

            // Se a conexão estiver fechada, abre a conexão
            if (SqlConnection.State == ConnectionState.Closed)
                SqlConnection.Open(); // Abre a conexão
        }

        // Método público para iniciar uma transação SQL
        public void OpenTransaction()
        {
            OpenConnection(); // Abre a conexão com o banco de dados
            SqlTransaction = SqlConnection.BeginTransaction(); // Inicia uma nova transação SQL
        }

        // Método público para iniciar uma transação SQL com um nível de isolamento específico
        public void OpenTransaction(IsolationLevel isolationLevel)
        {
            OpenConnection(); // Abre a conexão com o banco de dados
            SqlTransaction = SqlConnection.BeginTransaction(isolationLevel); // Inicia uma nova transação com o nível de isolamento especificado
        }

        // Método público para confirmar (commit) a transação atual
        public void Commit()
        {
            SqlTransaction.Commit(); // Confirma a transação
            SqlTransaction.Dispose(); // Libera os recursos da transação
        }

        // Método público para reverter (rollback) a transação atual
        public void Rollback()
        {
            SqlTransaction.Rollback(); // Reverte a transação
            SqlTransaction.Dispose(); // Libera os recursos da transação
        }

        // Implementação do método Dispose da interface IDisposable para liberar recursos
        public void Dispose()
        {
            SqlConnection.Close(); // Fecha a conexão com o banco de dados
        }
    }
}
