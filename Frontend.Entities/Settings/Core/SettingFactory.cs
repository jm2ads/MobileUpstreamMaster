namespace Frontend.Business.Settings
{
    public class SettingFactory
    {
        public Setting Create(int usuarioId)
        {
            return new Setting()
            {
                UsuarioActivoId = usuarioId
            };
        }

        public Setting Create()
        {
            return new Setting();
        }
    }
}
