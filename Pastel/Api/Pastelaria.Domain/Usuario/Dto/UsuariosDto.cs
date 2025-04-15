using Pastelaria.Domain.Endereco.Dto;
using Pastelaria.Domain.Telefone.Dto;
using System;
using System.Collections.Generic;
using Pastelaria.Domain.Perfil.Dto;
using Pastelaria.Domain.Tarefa.Dto;

namespace Pastelaria.Domain.Usuario.Dto
{
  public  class UsuariosDto
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ind_Ativo { get; set; }
        public PerfilDto Perfil { get; set; }
        public List<TelefoneDto> Telefones { get; set; } 
        public List<EnderecoDto> Enderecos { get; set; }
        public List<TarefaDto> Tarefas { get; set; }

    }
}
