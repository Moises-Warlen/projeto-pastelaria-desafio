using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Pastelaria.Web.Application.Infra
{
    // Classe base que representa uma resposta genérica
    public class Response
    {
        // Construtor padrão
        public Response()
        {

        }

        // Construtor que inicializa o código de status HTTP
        public Response(HttpStatusCode code)
        {
            Code = code;
            // Inicializa a lista de mensagens como uma coleção vazia
            Messages = Enumerable.Empty<string>();
        }

        // Construtor que inicializa o código de status HTTP e as mensagens
        public Response(HttpStatusCode code, IEnumerable<string> messages) : this(code)
        {
            Messages = messages ?? Enumerable.Empty<string>();
        }

        // Código de status HTTP da resposta
        public HttpStatusCode Code { get; set; }
        // Representação em string do código de status HTTP
        public string Status => Code.ToString();
        // Coleção de mensagens associadas à resposta
        public IEnumerable<string> Messages { get; set; }
        // Comprimento total (opcional)
        public int? TotalLength { get; set; }

        // Propriedade que indica se a resposta é bem-sucedida (status 200 OK)
        public bool Ok => Code == HttpStatusCode.OK;
    }

    // Classe que representa uma resposta genérica com um tipo de conteúdo específico
    public class Response<T> : Response
    {
        // Construtor padrão
        public Response()
        {

        }

        // Construtor que inicializa o código de status HTTP e o conteúdo
        public Response(HttpStatusCode code, T content) : base(code)
        {
            Content = content;
        }

        // Construtor que inicializa o código de status HTTP, o conteúdo e o comprimento total
        public Response(HttpStatusCode code, T content, int totalLength) : this(code, content)
        {
            TotalLength = totalLength;
        }

        // Construtor que inicializa o código de status HTTP, o conteúdo e as mensagens
        public Response(HttpStatusCode code, T content, IEnumerable<string> messages) : base(code, messages)
        {
            Content = content;
        }

        // Conteúdo da resposta
        public T Content { get; set; }
    }

    // Classe que representa uma resposta com um tipo de conteúdo específico e uma estrutura de mensagens mais complexa
    public class ResponseNode<T> : Response
    {
        // Construtor padrão
        public ResponseNode()
        {

        }

        // Construtor que inicializa o código de status HTTP e o conteúdo
        public ResponseNode(HttpStatusCode code, T content) : base(code)
        {
            Content = content;
        }

        // Construtor que inicializa o código de status HTTP, o conteúdo e o comprimento total
        public ResponseNode(HttpStatusCode code, T content, int totalLength) : this(code, content)
        {
            TotalLength = totalLength;
        }

        // Construtor que inicializa o código de status HTTP, o conteúdo e as mensagens
        public ResponseNode(HttpStatusCode code, T content, IEnumerable<string> messages) : base(code)
        {
            Content = content;
            // Inicializa a estrutura de mensagens mais complexa
            Messages = new MessagesNode
            {
                Content = messages
            };
        }

        // Conteúdo da resposta
        public T Content { get; set; }
        // Estrutura de mensagens mais complexa
        public new MessagesNode Messages { get; set; }

        // Classe interna que representa uma estrutura de mensagens mais complexa
        public class MessagesNode
        {
            // Coleção de mensagens
            public IEnumerable<string> Content { get; set; }
        }
    }
}
