using Frontend.Business.Commons;
using Frontend.Business.Usuarios.Dispositivos.Core;
using System;
using System.Threading.Tasks;

namespace Frontend.Business.Usuarios.Core
{
    public class UsuarioGenerator
    {
        private readonly IRepository<Usuario> repository;
        private readonly IRepository<UsuarioFuncionalidad> repositoryUsuarioFuncionalidad;
        private readonly DispositivoGenerator dispositivoGenerator;

        public UsuarioGenerator(IRepository<Usuario> repository, IRepository<UsuarioFuncionalidad> repositoryUsuarioRol, DispositivoGenerator dispositivoGenerator)
        {
            this.repository = repository;
            this.repositoryUsuarioFuncionalidad = repositoryUsuarioRol;
            this.dispositivoGenerator = dispositivoGenerator;
        }

        public async Task<Usuario> Generate(Usuario usuario)
        {
            try
            {
                return await repository.SaveWithChildren(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return usuario;
        }

        public async void DropData()
        {
            await repository.DropTableAsync();
        }
    }
}
