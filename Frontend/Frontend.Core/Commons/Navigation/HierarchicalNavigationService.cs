using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Navigation
{
    public class HierarchicalNavigationService : BaseNavigation, INavigationService
    {
        public void PushAsync<From,To>()
        {
            var page = CreatePage<To>();
            Application.Current.MainPage.Navigation.PushAsync(page, true);
        }

        public void PopAsync<T>()
        {
            Application.Current.MainPage.Navigation.PopAsync(true);
        }

        public void PushAsync<From,To>(object parameter)
        {
            AddParameter<To>(parameter);
            PushAsync<From,To>();
        }

        public void PushNextMasterDetailPage<From, To>()
        {
            throw new System.NotImplementedException();
        }

        public void PopNextMasterDetailPage<T>()
        {
            throw new System.NotImplementedException();
        }

        public void PushNextMasterDetailPage<From, To>(object parameter)
        {
            throw new System.NotImplementedException();
        }

        public void Pop()
        {
            throw new System.NotImplementedException();
        }

        public void PushFromAsync<From, To>()
        {
            throw new System.NotImplementedException();
        }

        public void PushFromAsync<From, To>(object parameter)
        {
            throw new System.NotImplementedException();
        }

        public void PopToRoot()
        {
            throw new System.NotImplementedException();
        }

        public void PushFromAsync(Type from, Type to)
        {
            throw new NotImplementedException();
        }

        public void PushFromAsync(Type from, Type to, object parameter)
        {
            throw new NotImplementedException();
        }

        public void PushFromAsync(Type from, IList<Type> to, object parameters)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(IList<Type> pageList, object parameter)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(IList<Type> pageList)
        {
            throw new NotImplementedException();
        }

        public void PushFromRootAsync<To>()
        {
            throw new NotImplementedException();
        }

        public void PushFromRootAsync(Type to)
        {
            throw new NotImplementedException();
        }

        public void PushFromRootAsync(IList<Type> to)
        {
            throw new NotImplementedException();
        }

        public void PushFromRootAsync(IList<Type> to, object parameters)
        {
            throw new NotImplementedException();
        }

        public void PushFromRootAsync<To>(object parameter)
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync<T>()
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync<T>(object parameter)
        {
            throw new NotImplementedException();
        }

        public Task PopModalAsync()
        {
            throw new NotImplementedException();
        }

        public Task PopModalAsync<To>(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
