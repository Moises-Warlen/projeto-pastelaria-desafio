using Pastelaria.Web.Application.Usuario.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pastelaria.Web.Application.Tarefa.Model
{
    public class TarefaModel
    {
        public int IdTarefa { get; set; }
        public IEnumerable<UsuarioModel> Usuario { get; set; }
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = " digite descrição da tarefa")]
        public string Descricao { get; set; }
        public DateTime DataAtribuicao { get; set; } = DateTime.Now;
        public DateTime? DataConclusao { get; set; }
        public int CriadorId { get; set; }
        public bool Ind_Ativo { get; set; }
    }

}
