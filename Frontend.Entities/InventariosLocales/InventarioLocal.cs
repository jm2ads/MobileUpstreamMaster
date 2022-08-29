using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.DetallesInventarioLocal;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.InventariosLocales
{
    public class InventarioLocal : SyncLocalEntity
    {
        public string NumeroProvisorio { get; set; }
        public string NumeroSAP { get; set; }
        public string ProvisorioAnterior { get; set; }
        public bool EsProvisorio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaRecuento { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string ComentarioRechazo { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? IdAlmacen { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(Centro))]
        public int IdCentro { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        //[ForeignKey(typeof(EstadoInventario))] 
        //public int EstadoInventarioId { get; set; }
        //[ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public EstadoInventario Estado { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int IdStockEspecial { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleInventarioLocal> DetallesInventario { get; set; }

        public string Codigo
        {
            get
            {
                if (EsProvisorio) return NumeroProvisorio;
                return NumeroSAP;
            }
        }

        public InventarioLocal()
        {
        }
    }
}
