using Frontend.Business.Almacenes;
using Frontend.Business.Centros;
using Frontend.Business.ClasesDeValoracion;
using Frontend.Business.GruposDeArticulos;
using Frontend.Core.Commons.Observables;

namespace Frontend.Core.Areas.Inventarios.Models
{
    public class ConsultaStockModel : NotificationObject
    {
        Centro centro;
        public Centro Centro
        {
            get { return centro; }
            set { SetProperty(ref centro, value); }
        }
        public ObservableRangeCollection<Centro> ListaCentro { get; set; }

        Almacen almacen;
        public Almacen Almacen
        {
            get { return almacen; }
            set { SetProperty(ref almacen, value); }
        }

        public ObservableRangeCollection<Almacen> ListaAlmacen { get; set; }

        string grupoDeArticulo;
        public string GrupoDeArticulo
        {
            get { return grupoDeArticulo; }
            set { SetProperty(ref grupoDeArticulo, value); }
        }
        private System.Collections.IList listaGrupoDeArticulo;
        public System.Collections.IList ListaGrupoDeArticulo
        {
            get { return listaGrupoDeArticulo; }
            set
            {
                SetProperty(ref listaGrupoDeArticulo, value);
            }
        }

        string codigo;
        public string Codigo
        {
            get { return codigo; }
            set { SetProperty(ref codigo, value); }
        }
        private System.Collections.IList listaCodigo;
        public System.Collections.IList ListaCodigo
        {
            get { return listaCodigo; }
            set
            {
                SetProperty(ref listaCodigo, value);
            }
        }

        string textoCorto;
        public string TextoCorto
        {
            get { return textoCorto; }
            set { SetProperty(ref textoCorto, value); }
        }

        private System.Collections.IList listaTextoCorto;
        public System.Collections.IList ListaTextoCorto
        {
            get { return listaTextoCorto; }
            set
            {
                SetProperty(ref listaTextoCorto, value);
            }
        }

        ClaseDeValoracion lote;
        public ClaseDeValoracion Lote
        {
            get { return lote; }
            set { SetProperty(ref lote, value); }
        }

        public ObservableRangeCollection<ClaseDeValoracion> ListaLote { get; set; }

        public ConsultaStockModel()
        {
            ListaCentro = new ObservableRangeCollection<Centro>();
            ListaAlmacen = new ObservableRangeCollection<Almacen>();
            ListaLote = new ObservableRangeCollection<ClaseDeValoracion>();
        }
    }
}
