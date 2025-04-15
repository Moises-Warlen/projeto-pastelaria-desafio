using System;

namespace Pastelaria.Repository.Infra.HandledExceptions
{
    // Definição da classe DbException que herda de Exception
    public class DbException : Exception
    {
        // Construtor que recebe uma mensagem de erro e a repassa para a classe base (Exception)
        public DbException(string message)
        {
            Message = message; // Define a propriedade Message com a mensagem recebida
        }

        // Sobrescreve a propriedade Message da classe base (Exception) para retornar a mensagem personalizada
        public override string Message { get; }
    }
}
