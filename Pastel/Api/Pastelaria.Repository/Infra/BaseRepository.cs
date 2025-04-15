using Pastelaria.Repository.Infra.Extensions;  
using Pastelaria.Repository.Infra.HandledExceptions; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Pastelaria.Repository.Infra
{
    public class BaseRepository
    {
        protected readonly IDatabaseConnection Connection;  // Conexão com o banco de dados
        protected SqlCommand SqlCommand { get; set; }  // Comando SQL para execução

        public BaseRepository(IDatabaseConnection connection)
        {
            Connection = connection;  // Inicializa a conexão no construtor
        }

        // Métodos para gerenciar transações
        public void OpenTransaction() => Connection.OpenTransaction();
        public void OpenTransaction(IsolationLevel isolationLevel) => Connection.OpenTransaction(isolationLevel);
        public void RollbackTransaction() => Connection.Rollback();
        public void CommitTransaction() => Connection.Commit();

        // Abre a conexão com o banco de dados
        protected void OpenConnection()
        {
            try
            {
                // Verifica se a conexão está quebrada e a reabre se necessário
                if (SqlCommand.Connection.State == ConnectionState.Broken)
                {
                    SqlCommand.Connection.Close();
                    SqlCommand.Connection.Open();
                }

                // Abre a conexão se estiver fechada
                if (SqlCommand.Connection.State == ConnectionState.Closed)
                    SqlCommand.Connection.Open();
            }
            catch (SqlException ex)
            {
                // Trata exceção específica de erro de conexão
                if (ex.Number == 53)
                    throw new DbException("Falha ao efetuar conexão com o Banco de Dados");
                throw;  // Lança outras exceções para serem tratadas externamente
            }
        }

        // Fecha a conexão com o banco de dados
        public void CloseConnection() => Connection.SqlConnection.Close();

        // Prepara o comando SQL para execução de uma stored procedure
        protected void ExecuteProcedure(object procedureName)
        {
            SqlCommand = new SqlCommand(procedureName.ToString(), Connection.SqlConnection, Connection.SqlTransaction)
            {
                CommandType = CommandType.StoredProcedure,  // Define como stored procedure
                CommandTimeout = 99999  // Define o timeout (tempo máximo de execução)
            };
        }

        // Adiciona parâmetros ao comando SQL
        protected void AddParameter(string parameterName, object parameterValue)
        {
            // Adiciona parâmetros com base no tipo de dado
            if (parameterValue is bool)
                SqlCommand.Parameters.Add(parameterName, SqlDbType.Bit).Value = parameterValue;
            else
                SqlCommand.Parameters.AddWithValue(parameterName, parameterValue);
        }

        // Adiciona parâmetro de saída ao comando SQL
        protected void AddParameterOutput(string parameterName, object parameterValue, DbType parameterType)
        {
            SqlCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.Output,
                Value = parameterValue,
                DbType = parameterType
            });
        }

        // Adiciona parâmetro de saída ao comando SQL (sobrecarga para tipos específicos)
        protected void AddParameterOutput(string parameterName, SqlDbType parameterType, int parameterSize)
        {
            SqlCommand.Parameters.Add(parameterName, parameterType, parameterSize);
            SqlCommand.Parameters[parameterName].Direction = ParameterDirection.Output;
        }

        // Adiciona parâmetro de retorno ao comando SQL
        protected void AddParameterReturn(string parameterName = "@RETURN_VALUE", DbType parameterType = DbType.Int16)
        {
            SqlCommand.Parameters.Add(new SqlParameter
            {
                ParameterName = parameterName,
                Direction = ParameterDirection.ReturnValue,
                DbType = parameterType
            });
        }

        // Obtém o valor do parâmetro de saída do comando SQL
        protected string GetParameterOutput(string parameter) => SqlCommand.Parameters[parameter].Value?.ToString();

        // Executa o comando SQL que não retorna dados (INSERT, UPDATE, DELETE)
        protected int ExecuteNonQuery()
        {
            try
            {
                OpenConnection();  // Abre a conexão antes de executar
                return SqlCommand.ExecuteNonQuery();  // Executa o comando e retorna o número de linhas afetadas
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);  // Trata exceções específicas do SQL
            }
        }

        // Executa o comando SQL que retorna um valor de retorno
        protected int ExecuteNonQueryWithReturn()
        {
            try
            {
                AddParameterReturn();  // Adiciona parâmetro de retorno
                OpenConnection();  // Abre a conexão
                SqlCommand.ExecuteNonQuery();  // Executa o comando
                return int.Parse(SqlCommand.Parameters["@RETURN_VALUE"].Value.ToString());  // Retorna o valor de retorno convertido para int
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);  // Trata exceções específicas do SQL
            }
        }

        // Executa o comando SQL que retorna um valor de retorno de tipo genérico
        protected T ExecuteNonQueryWithReturn<T>()
        {
            try
            {
                AddParameterReturn();  // Adiciona parâmetro de retorno
                OpenConnection();  // Abre a conexão
                SqlCommand.ExecuteNonQuery();  // Executa o comando
                var value = SqlCommand.Parameters["@RETURN_VALUE"].Value;  // Obtém o valor de retorno
                if (value == DBNull.Value)
                    return default(T);  // Retorna o valor padrão se for nulo

                // Converte o valor para o tipo genérico T
                return (T)Convert.ChangeType(value, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);  // Trata exceções específicas do SQL
            }
        }

        // Executa um comando SQL com parâmetros
        protected void Execute(object procedure, IEnumerable<Parameter> parameters)
        {
            ExecuteProcedure(procedure);  // Prepara a stored procedure
            foreach (var parameter in parameters)
                AddParameter(parameter.Name, parameter.Value);  // Adiciona parâmetros
            ExecuteNonQuery();  // Executa o comando sem retorno
        }

        // Executa um comando SQL com parâmetros e retorna um valor
        protected T ExecuteWithReturn<T>(object procedure, IEnumerable<Parameter> parameters)
        {
            ExecuteProcedure(procedure);  // Prepara a stored procedure
            foreach (var parameter in parameters)
                AddParameter(parameter.Name, parameter.Value);  // Adiciona parâmetros
            return ExecuteNonQueryWithReturn<T>();  // Executa o comando com retorno
        }

        // Obtém uma lista enumerável de resultados de uma consulta SQL
        protected IEnumerable<T> GetEnumerable<T>(object procedure, IEnumerable<Parameter> parameters) where T : class
        {
            ExecuteProcedure(procedure);  // Prepara a stored procedure
            foreach (var parameter in parameters)
                AddParameter(parameter.Name, parameter.Value);  // Adiciona parâmetros
            using (var r = ExecuteReader())
                return r.CastEnumerable<T>();  // Converte o resultado para uma lista enumerável de tipo T
        }

        // Obtém uma entidade específica de uma consulta SQL
        protected T GetEntity<T>(object procedure, IEnumerable<Parameter> parameters) where T : class
        {
            ExecuteProcedure(procedure);  // Prepara a stored procedure
            foreach (var parameter in parameters)
                AddParameter(parameter.Name, parameter.Value);  // Adiciona parâmetros
            using (var r = ExecuteReader())
                return r.Read() ? r.Cast<T>() : null;  // Lê a primeira linha do resultado e converte para tipo T
        }

        // Executa o comando SQL que retorna um leitor de dados
        protected IDataReader ExecuteReader()
        {
            try
            {
                OpenConnection();  // Abre a conexão
                return SqlCommand.ExecuteReader();  // Retorna o leitor de dados
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);  // Trata exceções específicas do SQL
            }
        }

        // Executa o comando SQL que retorna um leitor de dados com opções adicionais
        protected IDataReader ExecuteReader(CommandBehavior cb)
        {
            try
            {
                return SqlCommand.ExecuteReader(cb);  // Retorna o leitor de dados com comportamento específico
            }
            catch (SqlException ex)
            {
                throw HandleSqlException(ex);  // Trata exceções específicas do SQL
            }
        }

        // Método privado para tratar exceções do SQL
        private static Exception HandleSqlException(SqlException ex)
        {
            if (ex.Number == 1205)
                return new DbException("Serviço indisponível, favor repetir a operação em alguns minutos ou contatar o DDP.");

            return ex;  // Retorna a exceção original se não for um erro específico tratado
        }
    }
}
