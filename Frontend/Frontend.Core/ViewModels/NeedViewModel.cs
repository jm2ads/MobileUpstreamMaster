using Frontend.Core.IViewModels;
using Frontend.Core.IViews;

namespace Frontend.Core.ViewModels
{
    public class NeedViewModel : BaseViewModel, INeedViewModel
    {
        // private INeedView view;

        public NeedViewModel()
        {
           
        }

        public void SetNavigation(INeedView view)
        {
            // this.view = view;
        }
    }
}
