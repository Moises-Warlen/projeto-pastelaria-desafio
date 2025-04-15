using System.Linq;

namespace Pastelaria.Repository.Infra
{
    public class Parameter
    {
        /// <summary>
        /// Representa um parâmetro para ser utilizado em consultas ou comandos de banco de dados.
        /// </summary>
        /// <param name="name">Nome do parâmetro, que pode ser passado com ou sem '@' no início.</param>
        /// <param name="value">Valor do parâmetro, que não necessita de nenhum cast explícito.</param>
        public Parameter(string name, object value)
        {
            _name = name;
            Value = value;
        }

        private readonly string _name;

        /// <summary>
        /// Obtém o nome do parâmetro formatado corretamente para utilização em comandos de banco de dados.
        /// </summary>
        public string Name => $"{(_name.FirstOrDefault() == '@' ? "" : "@")}{_name}";

        /// <summary>
        /// Obtém o valor do parâmetro.
        /// </summary>
        public object Value { get; }
    }
}
