using Frontend.Azure.DTOs;
using Frontend.Business.Usuarios;
using Frontend.Business.Usuarios.Core;
using Frontend.Commons.Commons;
using System.Threading.Tasks;

namespace Frontend.Azure.Mappers
{
    public class UsuarioMapper
    {
        private readonly UsuarioFactory usuarioFactory;
        private readonly DispositivoMapper dispositivoMapper;

        public UsuarioMapper(UsuarioFactory usuarioFactory, DispositivoMapper dispositivoMapper)
        {
            this.usuarioFactory = usuarioFactory;
            this.dispositivoMapper = dispositivoMapper;
        }

        public async Task<Usuario> MapFromDto(UsuarioInputDto usuarioInputDto)
        {
            var usuario = usuarioFactory.Create();

            usuario.Id = usuarioInputDto.Id;
            usuario.IdRed = usuarioInputDto.IdRed;
            usuario.Nombre = usuarioInputDto.NombreUsuario;
            usuario.Token = usuarioInputDto.Token;

            usuario.Dispositivo = await dispositivoMapper.MapFromDto(usuarioInputDto.Dispositivo);

            return usuario;
        }


        public Usuario MapFromDto(UserAutenticationResponseDto userAutenticationResponseDto)
        {
            var usuario = usuarioFactory.Create();

            usuario.IdRed = userAutenticationResponseDto.data.UserInfo.UserLogin;
            usuario.Nombre = userAutenticationResponseDto.data.UserInfo.UserName;
            usuario.Token = userAutenticationResponseDto.data.Token;
            
            return usuario;
        }
        

        public async Task<UsuarioOutputDto> MapToDto(Usuario usuario)
        {
            var usuarioOutputDto = new UsuarioOutputDto();
            usuarioOutputDto.Password = usuario.Contraseña;
            usuarioOutputDto.User = usuario.IdRed;
            usuarioOutputDto.Application = ApplicationConstants.ApplicationNameSecurity;
            usuarioOutputDto.Token = usuario.Token;
            usuarioOutputDto.UserName = usuario.Nombre;

            if (usuario.Dispositivo != null)
            {
                var datosDispositivo = await dispositivoMapper.MapToDto(usuario.Dispositivo);
                usuarioOutputDto.Model = datosDispositivo.Modelo;
                usuarioOutputDto.Platform = datosDispositivo.Plataforma;
                usuarioOutputDto.Serial = datosDispositivo.Serial;
                usuarioOutputDto.Version = datosDispositivo.Version;
                usuarioOutputDto.Manufacturer = datosDispositivo.Fabricante;
                usuarioOutputDto.Uuid = datosDispositivo.Uuid;
            }
           
            return usuarioOutputDto;
        }
    }
}
