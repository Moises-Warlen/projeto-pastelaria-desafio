using System.ComponentModel.DataAnnotations;

namespace Pastelaria.Web.Application.Endereco.Model
{
   public class EnderecoModel
    {
        public int IdEndereco { get; set; }
        [Required(ErrorMessage = " digite seu Cep")]
        public string Cep { get; set; }
        [Required(ErrorMessage = " digite sua cidade")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = " digite seu Bairro")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = " digite seu Rua")]
        public string Rua { get; set; }
        [Required(ErrorMessage = " digite seu Numero")]
        public int Numero { get; set; }
        [Required(ErrorMessage = " digite seu Complemento")]
        public string Complemento { get; set; }
        public int IdUsuario { get; set; }
    }
}
