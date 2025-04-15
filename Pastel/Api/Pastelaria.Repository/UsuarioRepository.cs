using Pastelaria.Domain.Usuario;
using Pastelaria.Domain.Usuario.Dto;
using Pastelaria.Repository.Infra;
using Pastelaria.Repository.Infra.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;
using Pastelaria.Domain.Perfil.Dto;

namespace Pastelaria.Repository
{
    // Classe de repositório para gerenciar dados de usuários
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        // Construtor que injeta a conexão com o banco de dados
        public UsuarioRepository(IDatabaseConnection connection) : base(connection)
        {
        }

        // Enumeração que define os nomes dos procedimentos armazenados
        private enum Procedures
        {
            AdicionarUsuario,         // Inserir novo usuário
            AtualizarUsuario,         // Atualizar usuário existente
            DesativarUsuarioAssociados,  // Desativar usuário e seus associados
            ListarUsuariosAtivos,     // Listar usuários ativos
            ListarGestores,           // Listar gestores
            BuscarLogin               // Buscar usuário por login
        }

        // Obtém todos os usuários ativos
        public IEnumerable<UsuariosDto> Get()
        {
            ExecuteProcedure(Procedures.ListarUsuariosAtivos);  // Executa o procedimento armazenado para buscar usuários ativos
            var usuarios = new List<UsuariosDto>();
            using (var r = ExecuteReader())  // Executa o leitor para ler os resultados
            {
                while (r.Read())
                {
                    var usuario = r.Cast<UsuariosDto>();  // Converte os dados lidos para um objeto UsuariosDto
                  
                    usuario.Perfil = new PerfilDto()
                    {
                        Tipo = r.ReadAttr<string>("NomePerfil"),
                        IdPerfil = r.ReadAttr<int>("IdPerfil")
                    };
                    usuarios.Add(usuario);  // Adiciona o usuário à lista
                }

                return usuarios;  // Retorna a lista de usuários
            }
        }

        // Insere um novo usuário
        public int Post(UsuariosDto usuario)
        {
            ExecuteProcedure(Procedures.AdicionarUsuario); // Executa o procedimento armazenado para inserir um usuário
            // Adiciona parâmetros para os dados do usuário
            AddParameter("@Nome", usuario.Nome);
            AddParameter("@Email", usuario.Email);
            AddParameter("@Senha", usuario.Senha);
            AddParameter("@DataNascimento", usuario.DataNascimento);
            AddParameter("@Ind_Ativo", true);
            AddParameter("@IdPerfil", usuario.Perfil.IdPerfil);
            return ExecuteNonQueryWithReturn<int>();  // Executa o comando e retorna o ID do novo usuário
        }

        // Atualiza um usuário existente
        public void Put(UsuariosDto usuario)
        {
            ExecuteProcedure(Procedures.AtualizarUsuario);  // Executa o procedimento armazenado para atualizar um usuário
            // Adiciona parâmetros para os dados do usuário
            AddParameter("@IdUsuario", usuario.IdUsuario);
            AddParameter("@Nome", usuario.Nome);
            AddParameter("@Email", usuario.Email);
            AddParameter("@Senha", usuario.Senha);
            AddParameter("@DataNascimento", usuario.DataNascimento);
            AddParameter("@Ind_Ativo", true);
            AddParameter("@IdPerfil", usuario.Perfil.IdPerfil);

            ExecuteNonQuery();  // Executa o comando para atualizar o usuário
        }

        // Desativa um usuário e retorna as informações do usuário desativado
        public UsuariosDto PutDesativaUsuario(int IdUsuario)
        {
            ExecuteProcedure(Procedures.DesativarUsuarioAssociados);  // Executa o procedimento armazenado para desativar o usuário
            AddParameter("@IdUsuario", IdUsuario);  // Adiciona o parâmetro para o ID do usuário
            using (var r = ExecuteReader())  // Executa o leitor para ler os resultados
                return r.Read() ? r.Cast<UsuariosDto>() : null;  // Retorna o usuário desativado ou null se não encontrado
        }

        // Obtém um usuário por ID ou nome
        public UsuariosDto Get(int? IdUsuario = default(int?), string nome = null)
        {
            ExecuteProcedure(Procedures.ListarUsuariosAtivos);  // Executa o procedimento armazenado para buscar usuários ativos
            // Adiciona parâmetros para filtrar por ID ou nome
            AddParameter("@IdUsuario", IdUsuario);
            AddParameter("@Nome", nome);
            using (var r = ExecuteReader())  // Executa o leitor para ler os resultados
            {
                if (!r.Read())
                    return null;  // Retorna null se o usuário não for encontrado

                var usuario = r.Cast<UsuariosDto>();  // Converte os dados lidos para um objeto UsuariosDto
               
                usuario.Perfil = new PerfilDto()
                {
                    Tipo = r.ReadAttr<string>("NomePerfil"),
                    IdPerfil = r.ReadAttr<int>("IdPerfil")
                };
                return usuario;  // Retorna o usuário encontrado
            }
        }

        // Busca um usuário pelo login (email e senha)
        public UsuariosDto PostLogin(UsuariosDto usuario)
        {
            ExecuteProcedure(Procedures.BuscarLogin);  // Executa o procedimento armazenado para buscar o login do usuário
            AddParameter("@Email", usuario.Email);
            AddParameter("@Senha", usuario.Senha);

            using (var r = ExecuteReader())  // Executa o leitor para ler os resultados
            {
                if (!r.Read())
                    return null;  // Retorna null se o usuário não for encontrado

                var usuarioDto = r.Cast<UsuariosDto>();  // Converte os dados lidos para um objeto UsuariosDto
                usuarioDto.Perfil = new PerfilDto
                {
                    IdPerfil = r.ReadAttr<int>("IdPerfil"),
                };

                return usuarioDto;  // Retorna o usuário encontrado
            }
        }
      
    }
}
