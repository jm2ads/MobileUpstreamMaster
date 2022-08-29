using Xamarin.Forms;

namespace Frontend.Core.Commons.CustomRenders
{
    public class NumericEntry : Entry
    {
        public NumericEntry()
        {
            TextChanged += NumericEntry_TextChanged;
        }

        private void NumericEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                var entry = sender as Entry;
                entry.Text = "0";
            }
        }
    }
}
