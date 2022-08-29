using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Commons.CustomRenders
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmptyView : ContentView
    {
		public EmptyView ()
		{
			InitializeComponent ();
		}

        #region Title

        public static readonly BindableProperty TituloTextProperty = BindableProperty.Create<Label, string>(
          x => x.Text, String.Empty,
          defaultBindingMode: BindingMode.TwoWay,
          propertyChanged: TituloTextPropertyChanged
      );

        public string TitleText
        {
            get { return base.GetValue(TituloTextProperty).ToString(); }
            set { base.SetValue(TituloTextProperty, value); }
        }

        private static void TituloTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EmptyView)bindable;
            control.titleText.Text = newValue as string;
        }

        #endregion

        #region ActionCommand
        public static readonly BindableProperty ActionCommandProperty = BindableProperty.Create(
            propertyName: "ActionCommand",
            returnType: typeof(ICommand),
            declaringType: typeof(EmptyView),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ActionCommandPropertyChanged
        );

        public ICommand ActionCommand
        {
            get { return base.GetValue(ActionCommandProperty) as ICommand; }
            set { base.SetValue(ActionCommandProperty, value); }
        }

        private static void ActionCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EmptyView)bindable;
            control.actionButton.Command = newValue as ICommand;
        }
        #endregion


        #region ActionText
        public static readonly BindableProperty ActionTextProperty = BindableProperty.Create(
            propertyName: "ActionText",
            returnType: typeof(string),
            declaringType: typeof(EmptyView),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ActionTextPropertyChanged
        );

        public string ActionText
        {
            get { return base.GetValue(ActionCommandProperty) as string; }
            set { base.SetValue(ActionCommandProperty, value); }
        }

        private static void ActionTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EmptyView)bindable;
            control.actionButton.Text = newValue as string;
        }
        #endregion

        #region DetailProperty
        public static readonly BindableProperty DetailProperty = BindableProperty.Create<Label, string>(
           x => x.Text, String.Empty,
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: SubTitleTextPropertyChanged
       );


        public string DetailText
        {
            get { return base.GetValue(DetailProperty).ToString(); }
            set { base.SetValue(DetailProperty, value); }
        }

        private static void SubTitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EmptyView)bindable;
            control.detailText.Text = newValue as string;
        }

        #endregion

        #region Image

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(PickerWithIcon), string.Empty,BindingMode.TwoWay,null, ImagePropertyChanged);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }


        private static void ImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (EmptyView)bindable;
            control.imageEmpty.Source = newValue as string;
        }
        #endregion
    }
}