using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Frontend.Core.Commons.CustomRenders
{
    public class ZebraListView : ListView
    {
        public bool isRowEven = false;

        public ZebraListView() : base(ListViewCachingStrategy.RecycleElementAndDataTemplate)
        {
            ItemSelected += ListViewInventarios_ItemSelected;
            SeparatorVisibility = SeparatorVisibility.None;
        }

        public ZebraListView(ListViewCachingStrategy listViewCachingStrategy = ListViewCachingStrategy.RetainElement) : base(listViewCachingStrategy)
        {
            ItemSelected += ListViewInventarios_ItemSelected;
            SeparatorVisibility = SeparatorVisibility.None;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "ItemsSource")
            {
                ((INotifyCollectionChanged)ItemsSource).CollectionChanged +=
                    new NotifyCollectionChangedEventHandler(ListCollectionChanged);
            }
        }

        private void ListCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.isRowEven = false;
        }

        private void ListViewInventarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}
