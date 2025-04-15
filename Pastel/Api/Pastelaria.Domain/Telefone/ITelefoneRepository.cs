using Pastelaria.Domain.Telefone.Dto;  // Importação do namespace que contém o DTO de Telefone
using System.Collections.Generic;

namespace Pastelaria.Domain.Telefone
{
    public interface ITelefoneRepository  // Definição da interface ITelefoneRepository
    {
        IEnumerable<TelefoneDto> Get();
        IEnumerable<TelefoneDto>  Get(int? id = null, string nome = null);

        void Post(int idUsuario, TelefoneDto telefone);  

        void Delete(int id);
        void DeletePorUsuario(int id);
    }

  
}
