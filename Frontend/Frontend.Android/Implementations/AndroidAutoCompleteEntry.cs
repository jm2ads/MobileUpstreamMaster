using Android.Content;
using Android.Views.InputMethods;
using Android.Widget;
using Frontend.Core.Commons.CustomRenders;
using Frontend.Droid.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AutoCompleteEntry), typeof(AndroidAutoCompleteEntry))]
namespace Frontend.Droid.Implementations
{
    public class AndroidAutoCompleteEntry : ViewRenderer<AutoCompleteEntry, AutoCompleteTextView>
    {
        public AndroidAutoCompleteEntry(Context context) : base(context)
        {
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "ItemsSource")
            {
                UpdateAdapter((sender as AutoCompleteEntry).ItemsSource);
            }

            if (e.PropertyName == "IsFocused")
            {
                (sender as AutoCompleteEntry).Text = Control.Text;
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<AutoCompleteEntry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                       var control = new AutoCompleteTextView(Context);

                    if (!string.IsNullOrEmpty(e.NewElement.Placeholder))
                        control.Hint = e.NewElement.Placeholder;

                    e.NewElement.Text = control.Text;
                    control.ImeOptions = ImeAction.Search;
                    control.EditorAction += Control_EditorAction;
                    control.SetSingleLine(true);
                    SetNativeControl(control);
                }
                e.NewElement.TextChanged += NewElement_TextChanged;
                Control.ImeOptions = ImeAction.Search;
                UpdateAdapter(e.NewElement.ItemsSource);
            }
        }

        private void NewElement_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txt = sender as AutoCompleteEntry;
            Control.SetText(e.NewTextValue, string.IsNullOrWhiteSpace(e.NewTextValue));
        }

        private void Control_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            e.Handled = false;
            if (e.ActionId == ImeAction.Search)
            {
                Element.Text = Control.Text;
                Element.SearchCommand?.Execute(null);
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void UpdateAdapter(System.Collections.IList itemsSource)
        {
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(Context, Android.Resource.Layout.SimpleDropDownItem1Line, itemsSource);
            Control.Adapter = autoCompleteAdapter;
        }
    }
}