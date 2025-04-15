using Newtonsoft.Json;
using System.Net;
using System.Net.Http;

namespace Pastelaria.Web.Application.Infra.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public static string[] GetErrors(this HttpResponseMessage response)
        {
            return response.StatusCode == HttpStatusCode.BadRequest
                ? response.Content.DeserializeJson<string[]>()
                : new string[] { response.Content.DeserializeJsonAsString() };
        }

        public static Response AsResponse(this HttpResponseMessage request)
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<Response>(json);
            if (response.Code == 0)
                response = new Response(HttpStatusCode.InternalServerError, new[] { json });

            return response;
        }

        public static Response<T> AsResponse<T>(this HttpResponseMessage request)
        {
            var json = request.Content.ReadAsStringAsync().Result;

            var response = JsonConvert.DeserializeObject<Response<T>>(json);
            if (response.Code == 0)
                response = new Response<T>(HttpStatusCode.InternalServerError, default(T), new[] { json });

            return response;
        }

        public static ResponseNode<T> AsResponseNode<T>(this HttpResponseMessage request)
        {
            var json = request.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ResponseNode<T>>(json);
            if (response.Code == 0)
                response = new ResponseNode<T>(HttpStatusCode.InternalServerError, default(T), new[] { json });

            return response;
        }
    }
}