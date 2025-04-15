using Pastelaria.Domain.Teste;
using Pastelaria.Domain.Teste.Dto;
using Pastelaria.Repository.Infra;
using Pastelaria.Repository.Infra.Extensions;
using System.Collections.Generic;

namespace Pastelaria.Repository
{
    public class TesteRepository : BaseRepository, ITesteRepository
    {
        public TesteRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        private enum Procedures
        {
            GKSSP_SelDescricao,
            GKSSP_InsTeste
        }


        public IEnumerable<TesteDto> Get()
        {
            ExecuteProcedure(Procedures.GKSSP_SelDescricao);

            using (var r = ExecuteReader())
                return r.CastEnumerable<TesteDto>();
        }

        public void Post(TesteDto teste)
        {
            ExecuteProcedure(Procedures.GKSSP_InsTeste);
            AddParameter("@Num_SeqlNegativ", teste.Id);
            AddParameter("@Num_ChavParc", teste.Id);
            ExecuteNonQuery();
        }
    }
}