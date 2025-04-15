using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace Pastelaria.Api.Infra.Configuration
{
    public static class WebApiConfig
    {
        public static void Register(this HttpConfiguration config, IDependencyResolver dependencyResolver)
        {
            // Configura rotas baseadas em atributos de rota
            config.MapHttpAttributeRoutes();

            // Configuração da rota para o endpoint "/Ping"
            config.Routes.MapHttpRoute("Ping", "", new { controller = "Ping", action = "Get" });

            // Configuração da rota padrão da API
            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{action}", new { controller = "Ping", action = RouteParameter.Optional });

            // Limpa os formatters padrão e adiciona um novo para JSON
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore, // Ignora valores nulos ao serializar
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // Ignora loops de referência ao serializar
                    ContractResolver = new DefaultContractResolver
                    {
                        IgnoreSerializableAttribute = true // Ignora atributos [Serializable] durante a serialização
                    }
                }
            });

            // Define o resolvedor de dependências para a configuração da Web API
            config.DependencyResolver = dependencyResolver;

            // Inicia uma nova thread para monitorar o uso de memória do processo
            new Thread(() =>
            {
                while (true)
                {
                    var memoryUsage = 0D;
                    try
                    {
                        using (var process = Process.GetCurrentProcess())
                        {
                            process.Refresh();
                            memoryUsage = Math.Round(process.PrivateMemorySize64 / 1024D / 1024D, 1); // Calcula o uso de memória em MB
                        }

                        if (memoryUsage < 1)
                            continue; // Se o uso de memória for menor que 1 MB, continua aguardando
                    }
                    catch (Exception ex)
                    {
                        // Em caso de erro ao obter o uso de memória, poderia ser registrado em um arquivo de log
                        // Log.Write(@"C:\Log\Noc\Momentum", $"Log_{DateTime.Today:yy-MM-dd}", ex.ToString());
                    }

                    Thread.Sleep(TimeSpan.FromMinutes(10)); // Aguarda por 10 minutos antes de verificar novamente o uso de memória
                }
            }).Start(); // Inicia a thread para monitorar o uso de memória
        }
    }
}
