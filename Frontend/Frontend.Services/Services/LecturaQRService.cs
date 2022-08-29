using Frontend.Business.LecturaQRs;
using Frontend.Business.LecturaQRs.Core;
using Frontend.IServices.IServices;
using System.Threading.Tasks;

namespace Frontend.Services.Services
{
    public class LecturaQRService : ILecturaQRService
    {
        private readonly LecturaQRFactory lecturaQRFactory;
        private readonly IMaterialService materialService;
        private readonly IClaseDeValoracionService claseDeValoracionService;
        private readonly IAlmacenService almacenService;

        public LecturaQRService(LecturaQRFactory lecturaQRFactory, IMaterialService materialService, IClaseDeValoracionService claseDeValoracionService,
            IAlmacenService almacenService)
        {
            this.lecturaQRFactory = lecturaQRFactory;
            this.materialService = materialService;
            this.claseDeValoracionService = claseDeValoracionService;
            this.almacenService = almacenService;
        }

        public async Task<LecturaQR> GetLecturaQR(string lectura)
        {
            var lecturaQR = lecturaQRFactory.Create(lectura);
            var material = await materialService.GetByCodigo(lecturaQR.CodigoMaterial);
            var almacen = await almacenService.GetByCodigo(lecturaQR.Almacen);
            var lote = await claseDeValoracionService.GetByCodigo(lecturaQR.Lote);

            lecturaQR.MaterialId = material?.Id;
            lecturaQR.AlmacenId = almacen?.Id;
            lecturaQR.LoteId = lote?.Id;

            return lecturaQR;
        }
    }
}
