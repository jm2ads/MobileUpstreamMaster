using System.Threading.Tasks;

namespace Frontend.IServices.IServices
{
    public interface ISyncService
    {
        Task SyncDataBase(string idRed);
        Task SyncData();
        Task DropData();
        Task SyncDataParcial();
        Task UploadPedidos();
    }
}
