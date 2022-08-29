using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos
{
    public class Movimiento : SyncEntity
    {
        public static string Ingreso = "Ingreso";
        public static string Salida = "Salida";
        public static string Devolucion = "Devolución";
        public static string VentaInterna = "VentaInterna";
        public static string Traslado = "Traslado";

        public string Nombre { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ClaseDeMovimiento> ClasesDeMovimientos { get; set; }
    }
}
