using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Frontend.Core.Commons.CustomRenders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TitleNavigationBar : ContentView
    {
        public static readonly BindableProperty TitleTextBarProperty = BindableProperty.Create<Label, string>(
           x => x.Text, String.Empty,
           defaultBindingMode: BindingMode.TwoWay,
           propertyChanged: TitleTextBarPropertyChanged
       );

        public string TitleTextBar
        {
            get { return base.GetValue(TitleTextBarProperty).ToString(); }
            set { base.SetValue(TitleTextBarProperty, value); }
        }

        private static void TitleTextBarPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleNavigationBar)bindable;
            control.titleBar.Text = newValue as string;
        }

        public static readonly BindableProperty ImageBackCommandProperty = BindableProperty.Create(
            propertyName: "ImageBackCommand",
            returnType: typeof(ICommand),
            declaringType: typeof(TitleNavigationBar),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ImageTapCommandPropertyChanged
        );

        public ICommand ImageBackCommand
        {
            get { return base.GetValue(ImageBackCommandProperty) as ICommand; }
            set { base.SetValue(ImageBackCommandProperty, value); }
        }

        private static void ImageTapCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleNavigationBar)bindable;
            control.imageTap.Command = newValue as ICommand;
        }

        public static readonly BindableProperty ShowImageProperty = BindableProperty.Create(
            propertyName: "ShowImage",
            returnType: typeof(bool),
            defaultValue: true,
            declaringType: typeof(TitleNavigationBar),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ShowImagePropertyChanged
        );

        public bool ShowImage
        {
            get { return (bool)GetValue(ShowImageProperty); }
            set { base.SetValue(ShowImageProperty, value); }
        }

        private static void ShowImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleNavigationBar)bindable;
            control.Image.IsVisible = (bool)newValue;
        }

        public void SetTitle(string title)
        {
            TitleTextBar = title;
        }

        public TitleNavigationBar()
        {
            InitializeComponent();
        }
    }
}