using Pastelaria.Domain.Usuario.Dto;
using System.Collections.Generic;

namespace Pastelaria.Domain.Usuario
{
    public interface IUsuarioRepository
    {
        // Método para obter todos os usuários
        // Retorna uma lista de objetos UsuariosDto representando todos os usuários
        IEnumerable<UsuariosDto> Get();

        // Método para obter um usuário específico
        // Aceita um parâmetro opcional 'id' e/ou 'nome'
        // Retorna um objeto UsuariosDto que corresponde ao 'id' ou 'nome'
        UsuariosDto Get(int? id = null, string nome = null);

        // Método para adicionar um novo usuário
        // Aceita um objeto UsuariosDto representando o novo usuário a ser adicionado
        // Retorna o ID do usuário recém-adicionado
        int Post(UsuariosDto usuario);

        // Método para desativar um usuário
        // Aceita um parâmetro 'IdUsuario' representando o ID do usuário a ser desativado
        // Retorna o objeto UsuariosDto do usuário desativado
        UsuariosDto PutDesativaUsuario(int IdUsuario);

        // Método para atualizar um usuário existente
        // Aceita um objeto UsuariosDto representando o usuário com as atualizações
        void Put(UsuariosDto usuario);


        // Método para autenticar um usuário
        // Aceita um objeto UsuariosDto com as credenciais de login
        // Retorna um objeto UsuariosDto com informações do usuário autenticado
        UsuariosDto PostLogin(UsuariosDto usuario);
    }
}
