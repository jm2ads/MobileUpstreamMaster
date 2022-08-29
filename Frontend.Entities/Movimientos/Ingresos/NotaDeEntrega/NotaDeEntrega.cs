using Frontend.Business.Synchronizer;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Movimientos.Ingresos
{
    public class NotaDeEntrega : SyncLocalEntity
    {
        public string NumeroNotaDeEntrega { get; set; }
        public string CartaDePorte{ get; set; }
        public string TextoDeCabecera{ get; set; }
        public string UsuarioCreacion{ get; set; }
        public DateTime FechaDocumento { get; set; }
        public DateTime FechaContabilizacion{ get; set; }

        [ForeignKey(typeof(Pedido))]
        public int PedidoId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Pedido Pedido { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleNotaDeEntrega> DetalleNotaDeEntrega { get; set; }
    }
}
