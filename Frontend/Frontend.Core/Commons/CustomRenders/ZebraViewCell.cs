using Xamarin.Forms;

namespace Frontend.Core.Commons.CustomRenders
{
    public class ZebraViewCell : ViewCell
    {
        public ZebraViewCell()
        {
            this.Appearing += Cell_OnAppearing;
        }

        private void Cell_OnAppearing(object sender, System.EventArgs e)
        {
            var viewCell = (ViewCell)sender;
            if (viewCell.View != null)
            {
                if (((ZebraListView)Parent).isRowEven)
                {
                    viewCell.View.BackgroundColor = Color.LightGray;
                }
                else
                {
                    viewCell.View.BackgroundColor = Color.White;
                }
            }

            ((ZebraListView)Parent).isRowEven = !((ZebraListView)Parent).isRowEven;
        }
    }
}
