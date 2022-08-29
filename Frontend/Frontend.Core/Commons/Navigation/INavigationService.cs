using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Core.Commons.Navigation
{
    public interface INavigationService
    {
        void PopAsync<T>();

        void PushAsync<From,To>();

        void PushAsync<From,To>(object parameter);

        void PushNextMasterDetailPage<From,To>();

        void PushFromAsync<From, To>();

        void PushFromAsync<From, To>(object parameter);

        void PushFromAsync(Type from, Type to);

        void PushFromAsync(Type from, Type to,object parameter);

        void PushFromAsync(Type from, IList<Type> to, object parameters);

        void PushFromRootAsync<To>();

        void PushFromRootAsync(Type to);

        void PushFromRootAsync(IList<Type> to);

        void PushFromRootAsync(IList<Type> to, object parameters);

        void PushFromRootAsync<To>(object parameter);

        void PopNextMasterDetailPage<T>();

        object GetNavigationParams<T>();

        void PushNextMasterDetailPage<From, To>(object parameter);

        void Pop();

        void PopToRoot();

        Task PushAsync(IList<Type> pageList, object parameter);

        Task PushAsync(IList<Type> pageList);
        Task PushModalAsync<T>();
        Task PushModalAsync<T>(object parameter);
        Task PopModalAsync();
        Task PopModalAsync<To>(object parameter);
        void DropNavigationParams<T>();
    }
}
