using System.Collections.Generic;
using Pastelaria.Web.Application.Endereco.Model;
using Pastelaria.Web.Application.Infra;
using Pastelaria.Web.Application.Infra.Extensions;

namespace Pastelaria.Web.Application.Endereco
{
    // Classe que representa a aplicação de Endereço
    public class EnderecoApplication : BaseApplication
    {
        // Construtor da classe, inicializa a URL base da API de Endereço
        public EnderecoApplication() : base($"http://localhost:51169/api/endereco")
        {
        }

        // Método para enviar um novo endereço (POST) para a API
        public Response Post(EnderecoModel endereco)
        {
            return Request.Post(endereco).AsResponse();
        }

        // Método para excluir um endereço existente (DELETE) da API usando o ID
        public Response Delete(int id)
        {
            return Request.Delete(id).AsResponse();
        }

        // Método para obter todos os endereços (GET) da API
        public Response<IEnumerable<EnderecoModel>> GetEndereços()
        {
            return Request.Get("todos").AsResponse<IEnumerable<EnderecoModel>>();
        }

        // Método para obter um endereço específico (GET) da API usando o ID
        public Response<IEnumerable<EnderecoModel>> GetEndereco(int id)
        {
            return Request.Get(new { id }).AsResponse<IEnumerable<EnderecoModel>>();
        }
    }
}
