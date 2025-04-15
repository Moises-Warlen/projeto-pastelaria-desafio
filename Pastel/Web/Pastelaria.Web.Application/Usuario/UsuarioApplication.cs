using Pastelaria.Web.Application.Infra;
using Pastelaria.Web.Application.Infra.Extensions;
using Pastelaria.Web.Application.Usuario.Model;
using System.Collections.Generic;
// Classe que herda de BaseApplication, especializada para operações com usuários
public class UsuarioApplication : BaseApplication
{
    // Construtor da classe, inicializa a URL base para a API de usuários
    public UsuarioApplication() : base($"http://localhost:51169/api/usuario")
    {
    }

    // Método para criar um novo usuário, enviando uma requisição POST
    public Response Post(UsuarioModel usuario)
    {
        // Envia o modelo de usuário como corpo da requisição POST e retorna a resposta
        return Request.Post(usuario).AsResponse();
    }

    // Método para autenticar um usuário, enviando uma requisição POST para o endpoint de login
    public Response<UsuarioModel> PostLogin(UsuarioModel usuario)
    {
        // Envia o modelo de usuário para o endpoint de login e retorna a resposta como UsuarioModel
        var response = Request.Post("login", usuario).AsResponse<UsuarioModel>();
        return response;
    }

    // Método para atualizar um usuário (não implementado)
    public void Put(UsuarioModel usuario)
    {
        // A implementação do método Put deve ser adicionada aqui para atualizar um usuário existente
    }

    // Método para deletar um usuário pelo ID, enviando uma requisição DELETE
    public Response Delete(int id)
    {
        // Envia uma requisição DELETE para o endpoint com o ID do usuário e retorna a resposta
        Request.Delete(id).AsResponse();
        return Request.Delete(id).AsResponse();
    }

    // Método para obter uma lista de todos os usuários, enviando uma requisição GET para o endpoint "todos"
    public Response<IEnumerable<UsuarioModel>> GetUsuarios()
        => Request.Get("todos").AsResponse<IEnumerable<UsuarioModel>>();

    // Método para obter um usuário específico pelo ID, enviando uma requisição GET
    public Response<UsuarioModel> GetUsuario(int id)
        => Request.Get(new { idUsuario = id }).AsResponse<UsuarioModel>();
}
