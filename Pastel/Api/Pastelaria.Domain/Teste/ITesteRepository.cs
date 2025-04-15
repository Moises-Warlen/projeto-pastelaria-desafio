
using Pastelaria.Domain.Teste.Dto;

using System.Collections.Generic;


namespace Pastelaria.Domain.Teste
{
    // Definindo a interface do repositório de teste
    public interface ITesteRepository
    {
        // Método para obter uma coleção de objetos TesteDto
        IEnumerable<TesteDto> Get();

        // Método para adicionar um objeto TesteDto ao repositório
        void Post(TesteDto teste);
    }
}
