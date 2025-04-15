using Pastelaria.Web.Application.Infra;
using Pastelaria.Web.Application.Infra.Extensions;
using Pastelaria.Web.Application.Telefone.Model;
using System.Collections.Generic;

namespace Pastelaria.Web.Application.Telefone
{
    public class TelefoneApplication : BaseApplication
    {
        // Construtor da classe TelefoneApplication que chama o construtor da classe base 
        // e define a URL base para a API de telefones.
        public TelefoneApplication() : base($"http://localhost:51169/api/telefone")
        {
        }

        // Método para enviar uma solicitação POST à API para criar um novo telefone.
        // Recebe um objeto TelefoneModel como parâmetro e retorna um objeto Response.
        public Response Post(TelefoneModel telefone)
        {
            return Request.Post(telefone).AsResponse();
        }

        // Método para enviar uma solicitação DELETE à API para excluir um telefone existente.
        // Recebe um ID do telefone a ser excluído e retorna um objeto Response.
        public Response Delete(int id)
        {
            return Request.Delete(id).AsResponse();
        }

        // Método para enviar uma solicitação GET à API para obter todos os telefones.
        // Retorna um objeto Response contendo uma coleção de TelefoneModel.
        public Response<IEnumerable<TelefoneModel>> GetTelefones() =>
            Request.Get("todos").AsResponse<IEnumerable<TelefoneModel>>();

        // Método para enviar uma solicitação GET à API para obter um telefone específico com base no ID.
        // Recebe um ID e retorna um objeto Response contendo uma coleção de TelefoneModel.
        public Response<IEnumerable<TelefoneModel>> GetTelefone(int id) =>
            Request.Get(new { id }).AsResponse<IEnumerable<TelefoneModel>>();
    }

}
