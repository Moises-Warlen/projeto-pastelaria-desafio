using Pastelaria.Repository.Infra;
using System.Collections.Generic;
using Pastelaria.Domain.Perfil;
using Pastelaria.Domain.Perfil.Dto;
using Pastelaria.Repository.Infra.Extensions;

namespace Pastelaria.Repository
{
    // A classe PerfilRepository é responsável pela comunicação com o repositório de dados relacionados ao perfil.
    public class PerfilRepository : BaseRepository, IPerfilRepository
    {
        // Construtor que injeta a conexão com o banco de dados.
        // Isso permite que a classe use a conexão para executar comandos SQL.
        public PerfilRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        // Enumeração que define os nomes dos procedimentos armazenados.
        // Usada para referenciar procedimentos armazenados no banco de dados.
        private enum Procedures
        {
            ListarTodosPerfil  // Procedimento armazenado para listar todos os perfis.
        }

        // Método que recupera uma lista de perfis do banco de dados.
        public IEnumerable<PerfilDto> Get()
        {
            // Executa o procedimento armazenado para listar todos os perfis.
            ExecuteProcedure(Procedures.ListarTodosPerfil);

            // Usa um leitor de dados para ler o resultado do procedimento armazenado.
            // O resultado é convertido para uma coleção de objetos PerfilDto.
            using (var r = ExecuteReader())
                return r.CastEnumerable<PerfilDto>();
        }
    }
}
