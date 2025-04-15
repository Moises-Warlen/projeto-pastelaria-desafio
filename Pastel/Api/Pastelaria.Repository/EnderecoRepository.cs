using System.Collections.Generic;
using Pastelaria.Domain.Endereco;
using Pastelaria.Domain.Endereco.Dto;
using Pastelaria.Repository.Infra;
using Pastelaria.Repository.Infra.Extensions;

namespace Pastelaria.Repository
{
    // A classe EnderecoRepository é responsável por interagir com o banco de dados
    // para realizar operações CRUD relacionadas a endereços.
    public class EnderecoRepository : BaseRepository, IEnderecoRepository
    {
        // Construtor que inicializa a conexão com o banco de dados através da base class.
        public EnderecoRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        // Enum para listar as stored procedures usadas nas operações CRUD.
        private enum Procedures
        {
            ListarEnderecos,          // Stored procedure para listar endereços
            DeletarEnderecoUsuario,   // Stored procedure para deletar endereços de um usuário específico
            DeletarEndereco,          // Stored procedure para deletar um endereço específico
            AdicionarEnderecoUsuario  // Stored procedure para adicionar um endereço a um usuário
        }

        // Método para obter todos os endereços, sem filtros.
        public IEnumerable<EnderecoDto> Get()
        {
            ExecuteProcedure(Procedures.ListarEnderecos); // Define a stored procedure a ser usada.
            using (var r = ExecuteReader()) // Executa a leitura da resposta da stored procedure.
                return r.CastEnumerable<EnderecoDto>(); // Converte o resultado para uma coleção de EnderecoDto.
        }

        // Sobrecarga do método Get para obter endereços com base no Id do usuário e/ou nome.
        public IEnumerable<EnderecoDto> Get(int? IdUsuario = default(int?), string nome = null)
        {
            ExecuteProcedure(Procedures.ListarEnderecos); // Define a stored procedure a ser usada.

            AddParameter("@IdUsuario", IdUsuario); // Adiciona o parâmetro IdUsuario à stored procedure.
            using (var r = ExecuteReader()) // Executa a leitura da resposta da stored procedure.
                return r.CastEnumerable<EnderecoDto>(); // Converte o resultado para uma coleção de EnderecoDto.
        }

        // Método para adicionar um endereço a um usuário específico.
        public void Post(int idUsuario, EnderecoDto endereco)
        {
            ExecuteProcedure(Procedures.AdicionarEnderecoUsuario); // Define a stored procedure a ser usada.
            AddParameter("@IdUsuario", idUsuario); // Adiciona o parâmetro IdUsuario à stored procedure.
            AddParameter("@Cep", endereco.Cep); // Adiciona o parâmetro Cep à stored procedure.
            AddParameter("@Cidade", endereco.Cidade); // Adiciona o parâmetro Cidade à stored procedure.
            AddParameter("@Bairro", endereco.Bairro); // Adiciona o parâmetro Bairro à stored procedure.
            AddParameter("@Rua", endereco.Rua); // Adiciona o parâmetro Rua à stored procedure.
            AddParameter("@Numero", endereco.Numero); // Adiciona o parâmetro Numero à stored procedure.
            AddParameter("@Complemento", endereco.Complemento); // Adiciona o parâmetro Complemento à stored procedure.
            ExecuteNonQuery();  // Executa o comando não-query para realizar a inserção no banco de dados.
        }

        // Método para deletar todos os endereços de um usuário específico.
        public void DeletePorUsuario(int id)
        {
            ExecuteProcedure(Procedures.DeletarEnderecoUsuario); // Define a stored procedure a ser usada.
            AddParameter("@IdUsuario", id); // Adiciona o parâmetro IdUsuario à stored procedure.
            ExecuteNonQuery(); // Executa o comando não-query para realizar a exclusão no banco de dados.
        }

        // Método para deletar um endereço específico com base no seu Id.
        public void Delete(int id)
        {
            ExecuteProcedure(Procedures.DeletarEndereco); // Define a stored procedure a ser usada.
            AddParameter("@IdEndereco", id); // Adiciona o parâmetro IdEndereco à stored procedure.
            ExecuteNonQuery(); // Executa o comando não-query para realizar a exclusão no banco de dados.
        }
    }
}
