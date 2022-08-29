using Frontend.Commons.Commons;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace Frontend.Business.Usuarios.Dispositivos.Core
{
    public class DispositivoFactory
    {
        public Dispositivo Create()
        {
            var deviceInfo = DependencyService.Get<IDeviceInformation>();
            return new Dispositivo()
            {
                Fabricante = deviceInfo.GetManufacturer(),
                Modelo = CrossDeviceInfo.Current.Model,
                Plataforma = SetPlataform(),
                Serial = deviceInfo.GetSerial(),
                Uuid = deviceInfo.GetUuid(),
                Version = CrossDeviceInfo.Current.Version
            };
        }

        private string SetPlataform()
        {
            switch (CrossDeviceInfo.Current.Platform)
            {
                case Plugin.DeviceInfo.Abstractions.Platform.Android:
                    return "Android";
                case Plugin.DeviceInfo.Abstractions.Platform.iOS:
                    return "IOS";
            }
            return "Unknow";
        }
    }
}
