namespace Pastelaria.Web.Application.Infra
{
    public abstract class BaseApplication
    {
        // Campo protegido que armazena uma instância da classe Request
        protected readonly Request Request;

        // Construtor que inicializa o campo Request com uma URI e um tempo de espera opcional
        protected BaseApplication(string uri, int timeout = 60000)
        {
            // Garante que a URI termina com uma barra, se não terminar, adiciona uma
            Request = new Request(uri.EndsWith("/") ? uri : uri + "/", timeout);
        }

        /// <summary>
        /// Define o tempo limite de requisição em milissegundos.
        /// </summary>
        /// <param name="timeout">Tempo limite em milissegundos (segundos multiplicado por 1000).</param>
        public void SetTimeout(int timeout) => Request.SetTimeout(timeout);
    }
}
