using Frontend.Azure.Common;
using Frontend.Azure.DTOs;
using Frontend.Azure.Mappers;
using Frontend.Business.IAzureRestServices;
using Frontend.Business.Usuarios;
using Frontend.Commons.Commons.Errors;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Frontend.Azure.RestServices
{
    public class UsuarioAzureRestService : RestService, IUserAzureRestService
    {
        #region Private Properties

        private readonly YpfAzureHttpClient client;
        private readonly UsuarioMapper usuarioMapper;

        #endregion

        #region Public Methods

        public UsuarioAzureRestService(YpfAzureHttpClient client, UsuarioMapper usuarioMapper)
        {
            this.client = client;
            this.usuarioMapper = usuarioMapper;
        }

        public async Task<Usuario> Login(Usuario usuario)
        {
            UsuarioOutputDto userDto = usuarioMapper.MapToDto(usuario).Result;
            CleanUser(ref userDto);
            var response = await client.Call<UserAutenticationResponseDto>(UrlConstants.LoginUser, userDto, HttpMethod.Post, null);
            return GetResponse(response, ValidationUserEnum.LoginRegister);
        }

        public async Task<Usuario> Register(Usuario usuario)
        {
            UsuarioOutputDto userDto = usuarioMapper.MapToDto(usuario).Result;
            CleanUser(ref userDto);
            var response = await client.Call<UserAutenticationResponseDto>(UrlConstants.RegisterUser, userDto, HttpMethod.Post, null);
            return GetResponse(response, ValidationUserEnum.LoginRegister);
        }

        public async Task<Usuario> Validate(Usuario usuario)
        {
            UsuarioOutputDto userDto = usuarioMapper.MapToDto(usuario).Result;
            CleanUser(ref userDto);
            var response = await client.Call<UserAutenticationResponseDto>(UrlConstants.ValidateToken, userDto, HttpMethod.Post, null);
            return GetResponse(response, ValidationUserEnum.TokenRefresh);
        }

        #endregion

        #region Private Methods

        private Usuario GetResponse(UserAutenticationResponseDto userAutenticationResponseDto, ValidationUserEnum type)
        {
            try
            {
                if (type == ValidationUserEnum.LoginRegister)
                {
                    ValidLoginRegisterResponse(userAutenticationResponseDto);
                }
                else
                {
                    ValidTokenRespose(userAutenticationResponseDto);
                }

                return usuarioMapper.MapFromDto(userAutenticationResponseDto);
            }
            catch (System.NullReferenceException e)
            {
                Crashes.TrackError(e);
                throw new AuthenticationException(BusinessErrorCode.ServicioSeguridadNoDisponible, "Servicio de seguridad no disponible. Por favor, vuelva a intentar mas tarde");
            }
        }

        private void ValidRespose(SeguridadResponseDto responseDto)
        {
            if (responseDto == null)
            {
            }
        }

        private void ValidTokenRespose(UserAutenticationResponseDto responseDto)
        {
            if (!responseDto.success)
            {
                throw new AuthenticationException(responseDto.errType == 0 ? BusinessErrorCode.ServicioSeguridadNoDisponible : responseDto.errType.ToString(), responseDto.message);
            }
        }

        private void ValidLoginRegisterResponse(UserAutenticationResponseDto responseDto)
        {
            if (!responseDto.success)
            {
                throw new BusinessException(responseDto.errType.ToString(), responseDto.message);
            }
        }

        private void CleanUser(ref UsuarioOutputDto userDto)
        {
            if (userDto.Uuid != null && userDto.Uuid.Contains("unknown")) userDto.Uuid = string.Empty;
            if (userDto.Serial != null && userDto.Serial.Contains("unknown")) userDto.Serial = string.Empty;
        }

        #endregion
    }
}
