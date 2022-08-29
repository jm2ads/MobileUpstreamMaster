using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Frontend.Core.Commons.CustomRenders
{
    public class AutoCompleteEntry : Entry
    {
        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(System.Collections.IList), typeof(AutoCompleteEntry), null, propertyChanged: ItemsSourcePropertyChanged);

        public static readonly BindableProperty SearchCommandProperty =
           BindableProperty.Create(nameof(SearchCommand), typeof(ICommand), typeof(AutoCompleteEntry), null, propertyChanged: SearchCommandPropertyChanged);


        private static void ItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCompleteEntry)bindable;
            control.ItemsSource = (System.Collections.IList)newValue;
        }

        private static void SearchCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AutoCompleteEntry)bindable;
            control.SearchCommand = (ICommand)newValue;
        }

        public System.Collections.IList ItemsSource
        {
            get { return (System.Collections.IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public ICommand SearchCommand
        {
            get { return (ICommand)GetValue(SearchCommandProperty); }
            set { SetValue(SearchCommandProperty, value); }
        }
    }
}
