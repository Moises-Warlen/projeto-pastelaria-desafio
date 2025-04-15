using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Pastelaria.Api.Infra
{
    public class Response
    {
        public Response()
        {

        }

        // Construtor que aceita apenas o código de status HTTP
        public Response(HttpStatusCode code)
        {
            Code = code;
            Messages = Enumerable.Empty<string>();  // Inicializa Messages como uma coleção vazia
        }

        // Construtor que aceita o código de status HTTP e mensagens
        public Response(HttpStatusCode code, IEnumerable<string> messages) : this(code)
        {
            Messages = messages ?? Enumerable.Empty<string>(); // Define as mensagens fornecidas ou uma coleção vazia se for nulo
        }

        // Propriedades da resposta
        public HttpStatusCode Code { get; set; }
        public string Status => Code.ToString();  // Propriedade somente leitura que retorna o status como string
        public IEnumerable<string> Messages { get; set; } // Mensagens associadas à resposta
        public int? TotalLength { get; set; } // Comprimento total (opcional) para casos como listagens paginadas

        public bool Ok => Code == HttpStatusCode.OK; // Propriedade de conveniência para verificar se o código é OK (200)
    }

    // Resposta genérica que inclui um conteúdo tipado
    public class Response<T> : Response
    {
        public Response()
        {

        }

        // Construtor que aceita código de status HTTP e conteúdo tipado
        public Response(HttpStatusCode code, T content) : base(code)
        {
            Content = content;
        }

        // Construtor que aceita código de status HTTP, conteúdo tipado e comprimento total
        public Response(HttpStatusCode code, T content, int totalLength) : this(code, content)
        {
            TotalLength = totalLength;
        }

        // Construtor que aceita código de status HTTP, conteúdo tipado e mensagens
        public Response(HttpStatusCode code, T content, IEnumerable<string> messages) : base(code, messages)
        {
            Content = content;
        }

        // Conteúdo da resposta
        public T Content { get; set; }
    }

    // Resposta especializada que inclui um conteúdo tipado e mensagens encapsuladas em um objeto separado
    public class ResponseNode<T> : Response
    {
        public ResponseNode()
        {

        }

        // Construtor que aceita código de status HTTP e conteúdo tipado
        public ResponseNode(HttpStatusCode code, T content) : base(code)
        {
            Content = content;
        }

        // Construtor que aceita código de status HTTP, conteúdo tipado e comprimento total
        public ResponseNode(HttpStatusCode code, T content, int totalLength) : this(code, content)
        {
            TotalLength = totalLength;
        }

        // Construtor que aceita código de status HTTP, conteúdo tipado e mensagens
        public ResponseNode(HttpStatusCode code, T content, IEnumerable<string> messages) : base(code)
        {
            Content = content;
            Messages = new MessagesNode // Inicializa Messages como um novo objeto MessagesNode
            {
                Content = messages // Define o conteúdo de mensagens no objeto MessagesNode
            };
        }

        // Conteúdo da resposta
        public T Content { get; set; }

        // Mensagens encapsuladas em um objeto separado
        public new MessagesNode Messages { get; set; }

        // Classe interna para encapsular as mensagens
        public class MessagesNode
        {
            public IEnumerable<string> Content { get; set; } // Conteúdo das mensagens
        }
    }
}
