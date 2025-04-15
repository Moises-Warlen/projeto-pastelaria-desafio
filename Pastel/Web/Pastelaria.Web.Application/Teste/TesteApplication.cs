using Pastelaria.Web.Application.Infra;
using Pastelaria.Web.Application.Infra.Extensions;
using Pastelaria.Web.Application.Teste.Model;
using System.Collections.Generic;

namespace Pastelaria.Web.Application.Teste
{
    public class TesteApplication : BaseApplication
    {
        public TesteApplication() : base($"http://localhost:51169/api/teste")
        {

        }

        public TesteModel Get() => new TesteModel { Id = 1, Descricao = "Teste de Aplicação" };

        public Response Post(TesteModel teste)
        {
            return Request.Post(teste).AsResponse();
        }


        public void Put(TesteModel teste)
        {

        }
        public void Delete(int identificador)
        {

        }

        public Response<IEnumerable<TesteModel>> GetTestes() => Request.Get("todos").AsResponse<IEnumerable<TesteModel>>();

     
    }
}