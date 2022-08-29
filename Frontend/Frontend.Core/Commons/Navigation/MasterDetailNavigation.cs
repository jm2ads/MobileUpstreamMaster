using System;
using System.Collections.Generic;
using Frontend.Core.Views;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Core.Commons.Navigation
{
    public class MasterDetailNavigation : BaseNavigation, INavigationService
    {
        private Dictionary<string, Type> historyPage = new Dictionary<string, Type>();

        private INavigation GetNavigation()
        {
            var currentPage = Application.Current.MainPage;
            return currentPage is MasterMenulview ? (currentPage as MasterMenulview).Detail.Navigation : (currentPage as NavigationPage).Navigation;
        }

        public void PopAsync<T>()
        {
            var currentPage = Application.Current.MainPage;
            if (currentPage is MasterMenulview)
            {
                (currentPage as MasterMenulview).Detail.Navigation.PopAsync(true);
            }
            else if (currentPage is NavigationPage)
            {
                (currentPage as NavigationPage).Navigation.PopAsync(true);
            }
        }

        public void PushAsync<From, To>()
        {
            var page = CreatePage<To>();
            var currentPage = Application.Current.MainPage;
            if (currentPage is MasterMenulview)
            {
                (currentPage as MasterMenulview).Detail.Navigation.PushAsync(page, true);
            }
            else if (currentPage is NavigationPage)
            {
                (currentPage as NavigationPage).Navigation.PushAsync(page, true);
            }
        }
        public async Task PushModalAsync<To>()
        {
            var page = CreatePage<To>();
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PushModalAsync<To>(object parameter)
        {
            AddParameter<To>(parameter);
            var page = CreatePage<To>();
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync<To>(object parameter)
        {
            AddParameter<To>(parameter);
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        
        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async void PushFromAsync<From, To>()
        {
            var page = CreatePage<To>();
            var currentPage = Application.Current.MainPage;
            INavigation navigation;

            if (currentPage is MasterMenulview)
            {
                navigation = (currentPage as MasterMenulview).Detail.Navigation;

            }
            else
            {
                navigation = (currentPage as NavigationPage).Navigation;
            }

            await navigation.PushAsync(page);

            var countPages = navigation.NavigationStack.Count - 1;

            while (countPages != 0 && navigation.NavigationStack[countPages - 1].GetType() != typeof(From))
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromAsync(Type from, Type to)
        {
            var page = CreatePage(to);
            var currentPage = Application.Current.MainPage;
            INavigation navigation = GetNavigation();

            await navigation.PushAsync(page);

            var countPages = navigation.NavigationStack.Count - 1;

            while (countPages != 0 && navigation.NavigationStack[countPages - 1].GetType() != from)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromAsync(Type from, IList<Type> to)
        {
            INavigation navigation = GetNavigation();
            await PushAsync(to);

            var countPages = navigation.NavigationStack.Count - to.Count;

            while (countPages != 0 && navigation.NavigationStack[countPages - 1].GetType() != from)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromAsync(Type from, IList<Type> to, object parameters)
        {
            INavigation navigation = GetNavigation();
            await PushAsync(to, parameters);

            var countPages = navigation.NavigationStack.Count - to.Count;

            while (countPages != 0 && navigation.NavigationStack[countPages - 1].GetType() != from)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromAsync<From, To>(object parameter)
        {
            AddParameter<To>(parameter);
            var page = CreatePage<To>();
            var currentPage = Application.Current.MainPage;
            INavigation navigation = GetNavigation();

            await navigation.PushAsync(page);

            var countPages = navigation.NavigationStack.Count - 1;

            while (countPages !=0 && navigation.NavigationStack[countPages - 1].GetType() != typeof(From))
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromRootAsync<To>()
        {
            var page = CreatePage<To>();
            var currentPage = Application.Current.MainPage;
            INavigation navigation = GetNavigation();

            await navigation.PushAsync(page);

            var countPages = navigation.NavigationStack.Count - 1;

            while (countPages != 0)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromRootAsync(Type to)
        {
            var page = CreatePage(to);
            var currentPage = Application.Current.MainPage;
            INavigation navigation = GetNavigation();

            await navigation.PushAsync(page);

            var countPages = navigation.NavigationStack.Count - 1;

            while (countPages != 0)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromRootAsync(IList<Type> to)
        {
            INavigation navigation = GetNavigation();
            await PushAsync(to);

            var countPages = navigation.NavigationStack.Count - to.Count;

            while (countPages != 0)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromRootAsync(IList<Type> to, object parameters)
        {
            INavigation navigation = GetNavigation();
            await PushAsync(to, parameters);

            var countPages = navigation.NavigationStack.Count - to.Count;

            while (countPages != 0)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async void PushFromRootAsync<To>(object parameter)
        {
            AddParameter<To>(parameter);
            var page = CreatePage<To>();
            var currentPage = Application.Current.MainPage;
            INavigation navigation = GetNavigation();

            await navigation.PushAsync(page);

            var countPages = navigation.NavigationStack.Count - 1;

            while (countPages != 0)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public async Task PushAsync(IList<Type> pageList, object parameter)
        {
            AddParameter(pageList.Last(), parameter);
            var lastPage = CreatePage(pageList.Last());
            INavigation navigation = GetNavigation();
            await navigation.PushAsync(lastPage);

            for (int i = pageList.Count - 2; i >= 0; i--)
            {
                var page = CreatePage(pageList[i]);

                navigation.InsertPageBefore(page, lastPage);
                lastPage = page;
            }
        }

        public async Task PushAsync(IList<Type> pageList)
        {
            var lastPage = CreatePage(pageList.Last());
            INavigation navigation = GetNavigation();
            await navigation.PushAsync(lastPage);

            for (int i = pageList.Count - 1; i >= 0; i--)
            {
                var page = CreatePage(pageList[i]);

                navigation.InsertPageBefore(page, lastPage);
            }
        }

        public async void PushFromAsync(Type from, Type to, object parameter)
        {
            AddParameter(to, parameter);
            var page = CreatePage(to);
            var currentPage = Application.Current.MainPage;
            INavigation navigation;

            if (currentPage is MasterMenulview)
            {
                navigation = (currentPage as MasterMenulview).Detail.Navigation;

            }
            else
            {
                navigation = (currentPage as NavigationPage).Navigation;
            }

            await navigation.PushAsync(page);

            var countPages = navigation.NavigationStack.Count - 1;

            while (navigation.NavigationStack[countPages - 1].GetType() != from)
            {
                navigation.RemovePage(navigation.NavigationStack[countPages - 1]);
                countPages--;
            }
        }

        public void PushAsync<From, To>(object parameter)
        {
            AddParameter<To>(parameter);
            PushAsync<From, To>();
        }

        public void PushNextMasterDetailPage<From, To>()
        {
            SaveHistory(typeof(From), typeof(To));
            var mainpage = Application.Current.MainPage as MasterMenulview;
            var page = CreatePage<To>();
            mainpage.Detail = new NavigationPage(page);
        }

        public void PushNextMasterDetailPage<From, To>(object parameter)
        {
            AddParameter<To>(parameter);
            PushNextMasterDetailPage<From, To>();
        }

        public void PopNextMasterDetailPage<T>()
        {
            var mainpage = Application.Current.MainPage as MasterMenulview;
            mainpage.Detail = GetLastPage<T>();
        }

        private Page GetLastPage<T>()
        {
            var key = typeof(T).Name;
            var lastPage = historyPage[key];
            return CreatePage(lastPage);
        }

        private void SaveHistory(Type from, Type to)
        {
            var key = to.Name;
            if (historyPage.ContainsKey(key))
            {
                historyPage[key] = from;
            }
            else
            {
                historyPage.Add(key, from);
            }
        }

        public void Pop()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }

        public void PopToRoot()
        {
            Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        
    }
}
