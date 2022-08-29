using Frontend.Commons.Bootstrapper;
using Frontend.Core.Commons.Container;
using System;
using System.Collections.Generic;
using Unity;
using Xamarin.Forms;

namespace Frontend.Core.Commons.Navigation
{
    public class BaseNavigation
    {
        protected readonly Dictionary<string, object> parameters = new Dictionary<string, object>();

        protected virtual Page CreatePage<T>()
        {
            return ContainerManager.Resolve(typeof(T)) as Page; 
        }

        protected virtual Page CreatePage(Type type)
        {
            return ContainerManager.Resolve(type) as Page;
        }

        protected virtual void AddParameter<T>(object parameter)
        {
            var key = typeof(T).Name;
            if (parameters.ContainsKey(key))
            {
                parameters[key] = parameter;
            }
            else
            {
                parameters.Add(key, parameter);
            }
        }

        protected virtual void AddParameter(Type type, object parameter)
        {
            var key = type.Name;
            if (parameters.ContainsKey(key))
            {
                parameters[key] = parameter;
            }
            else
            {
                parameters.Add(key, parameter);
            }
        }

        public virtual object GetNavigationParams<T>()
        {
            return parameters[typeof(T).Name];
        }

        public virtual void DropNavigationParams<T>()
        {
            parameters.Remove(typeof(T).Name);
        }
    }
}
