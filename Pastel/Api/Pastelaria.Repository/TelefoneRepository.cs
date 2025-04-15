using Pastelaria.Domain.Telefone;
using Pastelaria.Repository.Infra;
using Pastelaria.Domain.Telefone.Dto;
using System.Collections.Generic;
using Pastelaria.Repository.Infra.Extensions;

namespace Pastelaria.Repository
{
    public class TelefoneRepository : BaseRepository, ITelefoneRepository  // Definindo a classe e implementando a interface
    {
        public TelefoneRepository(IDatabaseConnection connection) : base(connection)  // Construtor para inicializar com conexão de banco de dados
        {
        }

        // Enumeração que define os procedimentos armazenados (supondo nomes de procedimentos)
        private enum Procedures
        {
                AdicionarTelefoneUsuario,
                DeletarTelefoneUsuario,
                DeletarTelefone,
                ListarTodosTelefones,
        }
        // Obter todos os telefone
        public IEnumerable<TelefoneDto> Get()
        {
            ExecuteProcedure(Procedures.ListarTodosTelefones);  
            using (var r = ExecuteReader())            
                return r.CastEnumerable<TelefoneDto>(); 
        }
        public void Post(int idUsuario, TelefoneDto telefone)
        {
            ExecuteProcedure(Procedures.AdicionarTelefoneUsuario);  // Chamando procedimento armazenado para adicionar telefone para usuário existente
            AddParameter("@Telefone", telefone.Telefone);
            AddParameter("@Tipo",telefone.Tipo);
            AddParameter("@IdUsuario", idUsuario);
            ExecuteNonQuery();  // Executando comando não-query (operação de inserção)
        }

        public IEnumerable<TelefoneDto>  Get(int? IdUsuario = default(int?), string nome = null)
        {
            ExecuteProcedure(Procedures.ListarTodosTelefones);

            AddParameter("@IdUsuario", IdUsuario);
            using (var r = ExecuteReader())
                return r.CastEnumerable<TelefoneDto>();
        }
      
        public void DeletePorUsuario(int id)
        {
            ExecuteProcedure(Procedures.DeletarTelefoneUsuario);
            AddParameter("@IdUsuario", id);
            ExecuteNonQuery();  // Executando comando não-query (operação de inserção)
        }

        public void Delete(int id)
        {
            ExecuteProcedure(Procedures.DeletarTelefone);
            AddParameter("@IdTelefone", id);
            ExecuteNonQuery();  // Executando comando não-query (operação de inserção)
        }
    }
}
