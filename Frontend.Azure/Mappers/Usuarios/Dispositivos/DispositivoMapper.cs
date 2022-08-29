using Frontend.Azure.DTOs;
using Frontend.Business.Usuarios.Dispositivos;
using Frontend.Business.Usuarios.Dispositivos.Core;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class DispositivoMapper
    {
        private readonly DispositivoFactory dispositivoFactory;

        public DispositivoMapper(DispositivoFactory dispositivoFactory)
        {
            this.dispositivoFactory = dispositivoFactory;
        }

        public async Task<Dispositivo> MapFromDto(DispositivoInputDto dispositivoInputDto)
        {
            var dispositivo = dispositivoFactory.Create();

            dispositivo.Id = dispositivoInputDto.Id;
            dispositivo.Fabricante = dispositivoInputDto.Fabricante;
            dispositivo.Modelo = dispositivoInputDto.Modelo;
            dispositivo.Plataforma = dispositivoInputDto.Plataforma;
            dispositivo.Serial = dispositivoInputDto.Serial;
            dispositivo.Uuid = dispositivoInputDto.Uuid;
            dispositivo.Version = dispositivoInputDto.Version;

            return dispositivo;
        }

        public async Task<DispositivoInputDto> MapToDto(Dispositivo dispositivoUsuario)
        {
            var dispositivoInputDto = new DispositivoInputDto();

            dispositivoInputDto.Fabricante = dispositivoUsuario.Fabricante;
            dispositivoInputDto.Modelo = dispositivoUsuario.Modelo;
            dispositivoInputDto.Plataforma = dispositivoUsuario.Plataforma;
            dispositivoInputDto.Serial = dispositivoUsuario.Serial;
            dispositivoInputDto.Uuid = dispositivoUsuario.Uuid;
            dispositivoInputDto.Version = dispositivoUsuario.Version;

            return dispositivoInputDto;
        }
    }
}
