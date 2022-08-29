using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.DetallesInventario;
using Frontend.Business.StocksEspeciales;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Enums;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Frontend.Business.Inventarios
{
    public class Inventario : SyncEntity
    {
        public string NumeroProvisorio { get; set; }
        public string ProvisorioAnterior { get; set; }
        public string NumeroSAP { get; set; }
        public bool EsProvisorio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaRecuento { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public EstadoInventario Estado { get; set; }
        public string ComentarioRechazo { get; set; }
        public string AlmacenCodigo { get; set; }
        public string StockEspecialDescripcion { get; set; }
        public bool HayDiferencia { get; set; }
        public bool HayConteoErroneo { get; set; }
        public EstadoConteoEnum EstadoConteo { get; set; }

        [ForeignKey(typeof(Almacen))]
        public int? IdAlmacen { get; set; }
        [ManyToOne(foreignKey: "IdAlmacen", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Almacen Almacen { get; set; }

        [ForeignKey(typeof(Centro))]
        public int IdCentro { get; set; }
        [ManyToOne(foreignKey: "IdCentro", CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public Centro Centro { get; set; }

        public int Ejercicio { get; set; }

        [ForeignKey(typeof(StockEspecial))]
        public int IdStockEspecial { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead, ReadOnly = true)]
        public StockEspecial StockEspecial { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<DetalleInventario> DetallesInventario { get; set; }

        public string Codigo {
            get
            {
                if (EsProvisorio) return NumeroProvisorio;
                if (string.IsNullOrEmpty(NumeroSAP)) return NumeroProvisorio;
                return NumeroSAP;
            }
        }

        public int inventarioLocalId;

        public Inventario()
        {
        }

        public override bool Equals(object obj)
        {
            return this.Codigo == (obj as Inventario).Codigo; 
        }

    }
}
