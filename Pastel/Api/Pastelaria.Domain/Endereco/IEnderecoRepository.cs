// Importa o namespace que contém a definição de EnderecoDto
using Pastelaria.Domain.Endereco.Dto;
// Importa o namespace para coleções genéricas, como IEnumerable
using System.Collections.Generic;

namespace Pastelaria.Domain.Endereco
{
    // Define uma interface para o repositório de endereços
    public interface IEnderecoRepository
    {
        // Método para obter uma coleção de objetos EnderecoDto
        IEnumerable<EnderecoDto> Get();

        // Método para obter uma coleção de objetos EnderecoDto, com possibilidade de filtrar por id ou nome
        IEnumerable<EnderecoDto> Get(int? id = null, string nome = null);

        // Método para adicionar um novo endereço para um usuário específico
        void Post(int idUsuario, EnderecoDto endereco);

        // Método para excluir um endereço pelo seu id
        void Delete(int id);

        // Método para excluir todos os endereços associados a um usuário específico
        void DeletePorUsuario(int id);
    }
}
