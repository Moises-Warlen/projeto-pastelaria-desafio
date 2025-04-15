
using System.Collections.Generic;
using Pastelaria.Web.Application.Perfil.Model;

namespace Pastelaria.Web.Application.Usuario.Model
{
    public class EditarUsuarioModel
    {
        public UsuarioModel Usuario { get; set; }
        public IEnumerable<PerfilModel> Perfil { get; set; }
       
    }
}
