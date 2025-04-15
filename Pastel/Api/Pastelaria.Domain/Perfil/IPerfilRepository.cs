// Importa a definição da classe PerfilDto do namespace Pastelaria.Domain.Perfil.Dto
using Pastelaria.Domain.Perfil.Dto;

// Importa a coleção genérica System.Collections.Generic, necessária para usar IEnumerable<T>
using System.Collections.Generic;

namespace Pastelaria.Domain.Perfil
{
    // Define a interface IPerfilRepository para operações relacionadas a perfis
    public interface IPerfilRepository
    {
        // Declara um método para obter uma coleção de objetos PerfilDto
        IEnumerable<PerfilDto> Get();
    }
}
