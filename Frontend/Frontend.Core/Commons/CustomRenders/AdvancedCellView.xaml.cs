using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Commons.CustomRenders
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdvancedCellView : ContentView
	{
        #region Titulo1 Property
        public static readonly BindableProperty Titulo1Property = BindableProperty.Create<Label, string>(
            x => x.Text, String.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TitleTextBarPropertyChanged
        );

        public string Titulo1
        {
            get { return base.GetValue(Titulo1Property).ToString(); }
            set { base.SetValue(Titulo1Property, value); }
        }

        private static void TitleTextBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AdvancedCellView)bindable;
            control.titulo1.Text = newValue as string;
        }
        #endregion

        #region Dato1 Property

        public static readonly BindableProperty Dato1Property = BindableProperty.Create<Label, string>(
            x => x.Text, String.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: Dato1PropertyChanged
        );

        public string Dato1
        {
            get { return base.GetValue(Dato1Property).ToString(); }
            set { base.SetValue(Dato1Property, value); }
        }

        private static void Dato1PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AdvancedCellView)bindable;
            control.dato1.Text = newValue as string;
        }
        #endregion

        #region Titulo2
        public static readonly BindableProperty Titulo2Property = BindableProperty.Create<Label, string>(
            x => x.Text, String.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: Titulo2PropertyChanged
        );

        public string Titulo2
        {
            get { return base.GetValue(Titulo2Property).ToString(); }
            set { base.SetValue(Titulo2Property, value); }
        }

        private static void Titulo2PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AdvancedCellView)bindable;
            control.titulo2.Text = newValue as string;
        }
        #endregion

        #region Dato2

        public static readonly BindableProperty Dato2Property = BindableProperty.Create<Label, string>(
            x => x.Text, String.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: Dato2PropertyChanged
        );

        public string Dato2
        {
            get { return base.GetValue(Dato2Property).ToString(); }
            set { base.SetValue(Dato2Property, value); }
        }

        private static void Dato2PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AdvancedCellView)bindable;
            control.dato2.Text = newValue as string;
        }
        #endregion

        #region Titulo3
        public static readonly BindableProperty Titulo3Property = BindableProperty.Create<Label, string>(
            x => x.Text, String.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: Titulo3PropertyChanged
        );

        public string Titulo3
        {
            get { return base.GetValue(Titulo3Property).ToString(); }
            set { base.SetValue(Titulo3Property, value); }
        }

        private static void Titulo3PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AdvancedCellView)bindable;
            control.titulo3.Text = newValue as string;
        }
        #endregion

        #region Dato3

        public static readonly BindableProperty Dato3Property = BindableProperty.Create<Label, string>(
            x => x.Text, String.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: Dato3PropertyChanged
        );

        public string Dato3
        {
            get { return base.GetValue(Dato3Property).ToString(); }
            set { base.SetValue(Dato3Property, value); }
        }

        private static void Dato3PropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AdvancedCellView)bindable;
            control.dato3.Text = newValue as string;
        }
        #endregion

        public AdvancedCellView ()
		{
			InitializeComponent ();
		}
	}
}