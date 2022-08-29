using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Commons.CustomRenders
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TituloSubtituloNavigationView : ContentView
    {
        #region Titulo

        public static readonly BindableProperty TituloTextProperty = BindableProperty.Create<Label, string>(
          x => x.Text, String.Empty,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: TituloTextPropertyChanged
      );

        public string TituloText
        {
            get { return base.GetValue(TituloTextProperty).ToString(); }
            set { base.SetValue(TituloTextProperty, value); }
        }

        private static void TituloTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TituloSubtituloNavigationView)bindable;
            control.tituloText.Text = newValue as string;
        }

        #endregion

        #region ImagenCommand
        public static readonly BindableProperty ImagenCommandProperty = BindableProperty.Create(
            propertyName: "ImagenCommand",
            returnType: typeof(ICommand),
            declaringType: typeof(TitleNavigationBar),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ImagenCommandPropertyChanged
        );

        public ICommand ImageBackCommand
        {
            get { return base.GetValue(ImagenCommandProperty) as ICommand; }
            set { base.SetValue(ImagenCommandProperty, value); }
        }

        private static void ImagenCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TituloSubtituloNavigationView)bindable;
            control.imagenCommand.Command = newValue as ICommand;
        }
        #endregion

        #region SubtituloProperty
        public static readonly BindableProperty SubtituloTextProperty = BindableProperty.Create<Label, string>(
           x => x.Text, String.Empty,
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: SubTitleTextPropertyChanged
       );


        public string SubtituloText
        {
            get { return base.GetValue(SubtituloTextProperty).ToString(); }
            set { base.SetValue(SubtituloTextProperty, value); }
        }

        private static void SubTitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TituloSubtituloNavigationView)bindable;
            control.subtituloText.Text = newValue as string;
        }

        #endregion

        public TituloSubtituloNavigationView ()
		{
			InitializeComponent ();
		}
	}
}