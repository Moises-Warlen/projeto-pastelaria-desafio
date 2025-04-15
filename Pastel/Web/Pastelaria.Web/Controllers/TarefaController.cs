using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pastelaria.Web.Application.Tarefa;
using Pastelaria.Web.Application.Tarefa.Model;
using Pastelaria.Web.Infra;

namespace Pastelaria.Web.Controllers
{
    [Authorize]
    public class TarefaController : BaseController
    {
        // Instâncias das aplicações e serviços injetados no controlador
        private readonly TarefaApplication _tarefaApplication;
        private readonly UsuarioApplication _usuarioApplication;
        private readonly EmailService _emailService;

        // Construtor do controlador
        public TarefaController(TarefaApplication tarefaApplication, UsuarioApplication usuarioApplication, EmailService emailService)
        {
            _tarefaApplication = tarefaApplication;
            _usuarioApplication = usuarioApplication;
            _emailService = emailService;
        }

        // Ação para exibir a página inicial do controlador
        public ActionResult Index()
        {
            return View();
        }

        // Ação para listar as tarefas
        public ActionResult ListaTarefas()
        {
            // Obtém o ID do perfil armazenado na sessão
            var idPerfilObj = Session["IdPerfil"];
            int? idPerfil = null;

            // Converte o ID do perfil para inteiro, se possível
            if (idPerfilObj != null)
            {
                int tempIdPerfil;
                if (int.TryParse(idPerfilObj.ToString(), out tempIdPerfil))
                {
                    idPerfil = tempIdPerfil;
                }
            }

            // Obtém a lista de tarefas
            var response = _tarefaApplication.GetTarefas();
            if (!response.Ok)
                return Error("Falha ao buscar Tarefas");

            IEnumerable<TarefaModel> tarefas = response.Content;

            // Filtra as tarefas com base no perfil do usuário, se necessário
            if (idPerfil.HasValue && idPerfil.Value == 0)
            {
                var userId = Session["Id"] as int?;
                if (userId.HasValue)
                {
                    tarefas = tarefas.Where(t => t.IdUsuario == userId.Value);
                }
            }

            // Retorna a visualização da lista de tarefas
            return View("_GridTarefas", tarefas);
        }

        // Ação para deletar uma tarefa
        public ActionResult Deletar(int id)
        {
            _tarefaApplication.Delete(id);
            TempData["tarefaexcluida"] = "Tarefa Excluida!";
            return RedirectToAction("ListaTarefas");
        }

        // Ação para marcar uma tarefa como concluída
        public ActionResult Concluir(int id)
        {
            // Primeiro, obtenha a tarefa que está sendo concluída.
            var tarefaResponse = _tarefaApplication.GetTarefas();
            if (!tarefaResponse.Ok)
                return Error("Falha ao buscar Tarefas");

            var tarefa = tarefaResponse.Content.FirstOrDefault(t => t.IdTarefa == id);
            if (tarefa == null)
                return Error("Tarefa não encontrada");

            // Marque a tarefa como concluída.
            _tarefaApplication.PutConcluirTarefa(id);

            // Recupere o usuário que criou a tarefa.
            var usuarioResponse = _usuarioApplication.GetUsuario(tarefa.CriadorId);
            if (!usuarioResponse.Ok)
                return Error("Falha ao buscar usuário");

            var usuario = usuarioResponse.Content;
            if (usuario == null)
                return Error("Usuário não encontrado");

            // Envie o e-mail para o criador da tarefa.
            var destinatario = usuario.Email; // E-mail do criador da tarefa
            var assunto = "Tarefa Concluída";
            var corpo = $"A tarefa '{tarefa.Descricao}' foi concluída.";

            _emailService.EnviarEmail(destinatario, assunto, corpo);

            TempData["MensagemConcluidoComSucesso"] = "Tarefa concluida com sucesso!";
            return RedirectToAction("ListaTarefas");
        }

        // Ação para exibir a página de adicionar uma nova tarefa
        public ActionResult Adicionar()
        {
            var usuariosResponse = _usuarioApplication.GetUsuarios();
            if (!usuariosResponse.Ok)
                return Error("Falha ao buscar usuários");

            var model = new TarefaModel
            {
                Usuario = usuariosResponse.Content
            };

            return View("AdicionarTarefa", model);
        }

        // Ação para adicionar uma nova tarefa
        [HttpPost]
        public ActionResult Post(TarefaModel tarefa)
        {
            var response = _tarefaApplication.Post(tarefa);
            if (!response.Ok)
                return Error("Falha ao inserir tarefa");

            // Buscar o usuário ao qual a tarefa foi atribuída
            var usuario = _usuarioApplication.GetUsuario(tarefa.IdUsuario);
            if (usuario == null)
                return Error("Usuário não encontrado.");

            // Enviar e-mail quando a tarefa for criada
            var destinatario = usuario.Content.Email; // Ajuste para o e-mail do usuário correto
            var assunto = "Nova Tarefa Criada";
            var corpo = $"A tarefa '{tarefa.Descricao}' foi atribuido a voce.";
            _emailService.EnviarEmail(destinatario, assunto, corpo);

            TempData["MensagemSucesso"] = "Tarefa cadastrada com sucesso!";

            return RedirectToAction("Index");
        }
    }
}
