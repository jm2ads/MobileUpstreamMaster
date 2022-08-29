namespace Frontend.Azure.DTOs
{
    public class MaterialInputDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string UnidadDeMedidaBase { get; set; }
        public string UnidadDeMedidaAlternativa1 { get; set; }
        public string UnidadDeMedidaAlternativa2 { get; set; }
        public string UnidadDeMedidaAlternativa3 { get; set; }
        public string UnidadDeMedidaAlternativa4 { get; set; }
        public int GrupoDeArticuloId { get; set; }
    }
}
