using System.Collections.ObjectModel;
using System.Linq;
using Pastelaria.Web.Application.Endereco;
using Pastelaria.Web.Application.Telefone;
using Pastelaria.Web.Infra;
using System.Web.Mvc;
using Pastelaria.Web.Application.Endereco.Model;
using Pastelaria.Web.Application.Perfil;
using Pastelaria.Web.Application.Telefone.Model;
using Pastelaria.Web.Application.Usuario.Model;
using System.Web.Security;
using System;

namespace Pastelaria.Web.Controllers
{
   // Apenas usuários autenticados podem acessar este controlador
    public class UsuarioController : BaseController
    {
        // Aplicações necessárias para manipulação dos dados dos usuários
        private readonly UsuarioApplication _usuarioApplication;
        private readonly TelefoneApplication _telefoneApplication;
        private readonly EnderecoApplication _enderecoApplication;
        private readonly PerfilApplication _perfilApplication;

        public UsuarioController(
            UsuarioApplication usuarioApplication,
            TelefoneApplication telefoneApplication,
            EnderecoApplication enderecoApplication,
            PerfilApplication perfilApplication)
        {
            _usuarioApplication = usuarioApplication;
            _telefoneApplication = telefoneApplication;
            _enderecoApplication = enderecoApplication;
            _perfilApplication = perfilApplication;
        }
        [Authorize]
        // Ação para exibir a página principal do controlador
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        // Ação para exibir o perfil do usuário
        public ActionResult profile()
        {
            // Recuperar dados da sessão
            var username = Session["Username"] as string;
            var email = Session["Email"] as string;
            var idObj = Session["Id"];
            var idPerfilObj = Session["IdPerfil"];

            // Tentar converter o ID e o ID do perfil para int, se não for possível, tratar como null
            int? id = null;
            int? idPerfil = null;

            if (idObj != null)
            {
                int tempId;
                if (int.TryParse(idObj.ToString(), out tempId))
                {
                    id = tempId;
                }
            }

            if (idPerfilObj != null)
            {
                int tempIdPerfil;
                if (int.TryParse(idPerfilObj.ToString(), out tempIdPerfil))
                {
                    idPerfil = tempIdPerfil;
                }
            }

            // Verificar se todos os dados necessários estão presentes
            if (username != null && email != null && id.HasValue && idPerfil.HasValue)
            {
                ViewBag.Username = username;
                ViewBag.Email = email;
                ViewBag.Id = id.Value; // Certifique-se de que é um int
                ViewBag.IdPerfil = idPerfil.Value; // Adicione esta linha
            }
            else
            {
                // Redirecionar para a página de login se o usuário não estiver logado
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        // Ação para listar todos os usuários
        [Authorize]
        public ActionResult ListaUsuarios()
        {
            var response = _usuarioApplication.GetUsuarios();
            if (!response.Ok)
                return Error("Falha ao buscar os usuarios");
            return View("_GridUsuarios", response.Content);
        }

        // Ação para buscar a tela de edição de um usuário
        [Authorize]
        public ActionResult BuscarTelaEditarUsuario(int? id = null)
        {

            var responsePerfil = _perfilApplication.GetPerfil();
            if (!responsePerfil.Ok)
                return Error("Falha ao buscar perfis de usuario");

            // Se não for passado um ID, criar um modelo vazio para a tela de edição
            if (!id.HasValue)
                return View("_Form", new EditarUsuarioModel
                {
                    Usuario = new UsuarioModel
                    {
                        Telefones = new Collection<TelefoneModel>(),
                        Enderecos = new Collection<EnderecoModel>()
                    },
                    Perfil = responsePerfil.Content
                });

            // Se o ID for passado, buscar o usuário, endereços e telefones e exibir na tela de edição
            var responseUsuario = _usuarioApplication.GetUsuario(id.Value);
            if (!responseUsuario.Ok)
                return Error("Falha ao buscar usuário");

            var responseEndereco = _enderecoApplication.GetEndereco(id.Value);
            if (!responseEndereco.Ok)
                return Error("Falha ao buscar endereço");

            var responseTelefone = _telefoneApplication.GetTelefone(id.Value);
            if (!responseTelefone.Ok)
                return Error("Falha ao buscar telefones");

            var usuario = responseUsuario.Content;
            usuario.Telefones = responseTelefone.Content.ToList();
            usuario.Enderecos = responseEndereco.Content.ToList();

           

            return View("_Form", new EditarUsuarioModel
            {
                Usuario = usuario,
                Perfil = responsePerfil.Content
            });
        }


        // Ação para desativar um usuário
        [Authorize]
        public ActionResult Desativar(int id)
        {
            _usuarioApplication.Delete(id);

            TempData["userDeletado"] = "Usuario Deletado";
            return RedirectToAction("ListaUsuarios");
        }
        // Ação para criar ou atualizar um usuário
        [Authorize]
        public ActionResult Post(UsuarioModel usuario)
        {

            // Verifica se o modelo é válido
            if (ModelState.IsValid)
            {
                // Verifica se todos os campos obrigatórios estão preenchidos
                if (string.IsNullOrWhiteSpace(usuario.Nome) ||
                    string.IsNullOrWhiteSpace(usuario.Email) ||
                    string.IsNullOrWhiteSpace(usuario.Senha) ||
                    usuario.DataNascimento == null || // Verifica se a data de nascimento não é nula
                    usuario.Perfil?.IdPerfil == null) 
                                                      
                    
                {
                    ModelState.AddModelError("", "Todos os campos devem estar preenchidos.");
                    
                    return View(usuario); // Retorne a view com os erros
                }

                // Verifica se o usuário já existe
                var usuarioExistente = _usuarioApplication.GetUsuario(usuario.IdUsuario);
                bool isNovoUsuario = true;
                if (usuario.IdUsuario != 0)
                {
                     isNovoUsuario = false;
                }
             
                

                // Processa o salvamento do usuário
                var response = _usuarioApplication.Post(usuario);
                if (response.Ok)

                // Mensagem de sucesso dependendo se é um novo usuário ou não
                TempData["Msguser"] = isNovoUsuario ? "Usuário adicionado com sucesso!" : "Usuário atualizado com sucesso!";
                return Success();
            }

            // Se o modelo não for válido, retorne a view com os erros
            return View(usuario);
        }

        [HttpPost]
        public ActionResult PostLogin(UsuarioModel usuario)
        {
            var response = _usuarioApplication.PostLogin(usuario);
            if (!response.Ok)
            {
                TempData["MensagemErro"] = "Email ou senha Invalido!!";
                // Adicione uma mensagem de erro
                ViewBag.ErrorMessage = "Usuário não encontrado.";
                return RedirectToAction("Index", "Login");
            }

            // Defina o cookie de autenticação
            FormsAuthentication.SetAuthCookie(usuario.Email, false);

            // Definir variáveis de sessão
            Session["Username"] = response.Content.Nome; // Ajuste conforme necessário
            Session["Email"] = response.Content.Email;
            Session["Id"] = response.Content.IdUsuario;
            Session["IdPerfil"] = response.Content.Perfil.IdPerfil; // Adicione esta linha

            // Redirecione para a página inicial
            return RedirectToAction("Index", "Home");
        }

        // Ação para realizar logout e limpar sessão
        [Authorize]
        public ActionResult Logout()
        {
            // Limpar todas as variáveis de sessão
            Session.Clear();

            // Se você estiver usando cookies de autenticação, remova-os
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                authCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(authCookie);
            }

            // Se estiver utilizando FormsAuthentication, faça o Logout
            FormsAuthentication.SignOut();

            // Redirecionar para a página de login
            return RedirectToAction("Index", "Login");
        }

        // Ação para atualizar um usuário (implementação necessária)
        [Authorize]
        public ActionResult Put(UsuarioModel usuario)
        {
            // Atualizar usuário (Implementar lógica conforme necessário)
            return Success();
        }
    }
}
