using Frontend.Azure.DTOs;
using Frontend.Business.Users.Devices;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Azure.Mappers
{
    public class AplicacionUsuarioMapper
    {
        public IList<AplicacionUsuario> Map(IList<AplicacionUsuarioDto> dto)
        {
            return dto.Select(Map).ToList();
        }

        public AplicacionUsuario Map(AplicacionUsuarioDto dto)
        {
            return new AplicacionUsuario()
            {
                Nombre = dto.AppName,
                Activo = dto.Active
            };
        }
    }
}
