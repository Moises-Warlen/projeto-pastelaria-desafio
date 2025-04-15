using System;

namespace Pastelaria.Domain.Tarefa.Dto
{
   public class TarefaDto
    {
        public int IdTarefa { get; set; }
        public int IdUsuario { get; set; }
        public string Descricao { get; set; }
        public DateTime DataAtribuicao { get; set; } = DateTime.Now;
        public DateTime? DataConclusao { get; set; }
        public int CriadorId { get; set; }
        public bool Ind_Ativo { get; set; }
    }
}
