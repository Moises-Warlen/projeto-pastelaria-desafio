

using System.ComponentModel.DataAnnotations;

namespace Pastelaria.Web.Application.Telefone.Model
{
   public class TelefoneModel
    {
        public int IdTelefone { get; set; }
        [Required(ErrorMessage = " digite seu Telefone")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = " digite tipo de telefone")]
        public string Tipo { get; set; }
        public int IdUsuario { get; set; }
    }
}
