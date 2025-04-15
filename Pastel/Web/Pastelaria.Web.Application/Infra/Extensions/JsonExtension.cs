
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Pastelaria.Web.Application.Infra.Extensions
{
    public static class JsonExtension
    {
        public static T DeserializeJson<T>(this HttpContent conteudo)
        {
            return JsonConvert.DeserializeObject<T>(conteudo.ReadAsStringAsync().Result);
        }

        public static string DeserializeJsonAsString(this HttpContent conteudo)
        {
            return conteudo.ReadAsStringAsync().Result.Replace(Environment.NewLine, string.Empty);
        }
    }
}