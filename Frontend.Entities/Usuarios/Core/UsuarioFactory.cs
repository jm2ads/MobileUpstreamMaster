using Frontend.Business.Funcionalidades;
using Frontend.Business.Usuarios.Dispositivos;
using Frontend.Business.Usuarios.Dispositivos.Core;
using System.Collections.Generic;

namespace Frontend.Business.Usuarios.Core
{
    public class UsuarioFactory
    {
        private readonly DispositivoFactory dispositivoFactory;

        public UsuarioFactory(DispositivoFactory dispositivoFactory)
        {
            this.dispositivoFactory = dispositivoFactory;
        }

        public Usuario Create(string idRed, string contraseña)
        {
            return new Usuario()
            {
                IdRed = idRed,
                Contraseña = contraseña,
                Dispositivo = dispositivoFactory.Create()
            };
        }

        public Usuario Create(string idRed, string contraseña, Dispositivo dispositivo)
        {
            return new Usuario()
            {
                IdRed = idRed,
                Contraseña = contraseña,
                Dispositivo = dispositivo
            };
        }

        public Usuario Create(string idRed, string contraseña, Dispositivo dispositivo, string token)
        {
            return new Usuario()
            {
                IdRed = idRed,
                Contraseña = contraseña,
                Dispositivo = dispositivo,
                Token = token
            };
        }

        public Usuario Create()
        {
            return new Usuario()
            {
                Dispositivo = dispositivoFactory.Create(),
                Funcionalidades = new List<Funcionalidad>()
            };
        }
    }
}
