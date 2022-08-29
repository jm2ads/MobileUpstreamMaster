using System;
using System.Collections.Generic;
using Unity;

namespace Frontend.Commons.Bootstrapper
{
    public class ContainerManager
    {
        private readonly static IUnityContainer container;

        public static IUnityContainer Container
        {
            get
            {
                return container;
            }
        }

        static ContainerManager()
        {
            container = new UnityContainer();
        }

        public static T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        public static object Resolve(Type type)
        {
            try
            {
                return Container.Resolve(type);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static IEnumerable<T> ResolveAll<T>() where T : class
        {
            return Container.ResolveAll<T>();
        }
    }
}
