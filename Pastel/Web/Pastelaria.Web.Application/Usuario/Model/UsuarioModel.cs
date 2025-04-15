using System;
using System.Collections.Generic;
using Pastelaria.Web.Application.Endereco.Model;
using Pastelaria.Web.Application.Perfil.Model;
using Pastelaria.Web.Application.Tarefa.Model;
using Pastelaria.Web.Application.Telefone.Model;
using System.ComponentModel.DataAnnotations;

namespace Pastelaria.Web.Application.Usuario.Model
{

    public class UsuarioModel 
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = " digite Nome do Usuario")]

        public string Nome { get; set; }
        [Required(ErrorMessage = " digite um email valido")]

        public string Email { get; set; }
        [Required(ErrorMessage = " digite a senha")]

        public string Senha { get; set; }
        [Required(ErrorMessage = " digite data de nascimento")]

        public DateTime DataNascimento { get; set; }
        public bool Ind_Ativo { get; set; }

        public PerfilModel Perfil { get; set; }
        public IEnumerable<TelefoneModel> Telefones { get; set; }
        public IEnumerable<EnderecoModel> Enderecos { get; set; }
        public List<TarefaModel> Tarefas { get; set; }

    }
}
