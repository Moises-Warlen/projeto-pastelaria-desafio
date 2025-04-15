using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace Pastelaria.Api.Infra.Configuration
{
    public class AuthApiController : ApiController
    {
        // Método para retornar uma resposta com um código de status e mensagens
        protected IHttpActionResult Content(HttpStatusCode statusCode, IEnumerable<string> messages)
        {
            // Chama o método base Content com um objeto Response contendo o status e as mensagens
            return base.Content(statusCode, new Response(statusCode, messages));
        }

        // Método para retornar uma resposta com um código de status, conteúdo genérico e comprimento total
        protected IHttpActionResult Content<T>(HttpStatusCode statusCode, T content, int totalLength)
        {
            // Chama o método base Content com um objeto Response<T> contendo o status, conteúdo e comprimento
            return base.Content(statusCode, new Response<T>(statusCode, content, totalLength));
        }

        // Método para retornar uma resposta com um código de status, conteúdo genérico e mensagens
        protected IHttpActionResult Content<T>(HttpStatusCode statusCode, T content, IEnumerable<string> messages)
        {
            // Chama o método base Content com um objeto Response<T> contendo o status, conteúdo e mensagens
            return base.Content(statusCode, new Response<T>(statusCode, content, messages));
        }

        // Sobrescreve o método base Ok() para retornar uma resposta Ok com um objeto Response vazio
        protected new IHttpActionResult Ok() => base.Ok(new Response(HttpStatusCode.OK));

        // Sobrescreve o método base Ok<T>(T content) para retornar uma resposta Ok com um objeto Response contendo o conteúdo
        protected new IHttpActionResult Ok<T>(T content) => Content(HttpStatusCode.OK, content, null);

        // Método para retornar uma resposta Ok com um conteúdo genérico e um comprimento total
        protected IHttpActionResult Ok<T>(T content, int totalLength) => Content(HttpStatusCode.OK, content, totalLength);

        // Sobrescreve o método base BadRequest(string message) para retornar uma resposta BadRequest com uma mensagem
        protected new IHttpActionResult BadRequest(string message) => Content(HttpStatusCode.BadRequest, new[] { message });

        // Sobrescreve o método base StatusCode(HttpStatusCode status) para retornar uma resposta com o código de status especificado
        protected new IHttpActionResult StatusCode(HttpStatusCode status) => base.Content(status, new Response(status));
    }
}
