using System;
using System.Collections.Generic;

namespace Frontend.Azure.DTOs
{
    public class DetalleNotaDeEntregaOutputDto
    {
        public Boolean EntregaFinal { get; set; }
        public string TextoPosicion { get; set; }
        public string Posicion { get; set; }
        public string Unidad { get; set; }
        public string PuestoDeDescarga { get; set; }
        public string DestinatarioMercancia { get; set; }
        public string TipoDeStock { get; set; }
        public int ClaseDeValoracionId { get; set; }
        public int AlmacenId { get; set; }
        public int MaterialId { get; set; }
        public int StockEspecialId { get; set; }
        public int CentroId { get; set; }
        public double Tolerancia { get; set; }
        public List<DetalleNotaDeEntregaPosicionOutputDto> DetallesNotaDeEntregaPosicion { get; set; }
    }
}
