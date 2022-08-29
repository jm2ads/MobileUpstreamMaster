using Frontend.Business.Attributes;
using Frontend.Business.Commons;
using Frontend.Business.Synchronizer;
using Frontend.Commons.Bootstrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Frontend.Data.Database
{
    public class DatabaseManager: IDatabaseManager
    {
        public void InitDB()
        {
            var assembly = typeof(IRepository<>).GetTypeInfo().Assembly;
            var entities = assembly.DefinedTypes
                                   .Where(x => (x.IsSubclassOf(typeof(PersistebleEntity)) || x.IsSubclassOf(typeof(LocalEntity)))
                                          && !x.IsAbstract);

            foreach (var entity in entities)
            {
                var genericRepoEntityType = typeof(IRepository<>).MakeGenericType(entity.AsType());
                var repoEntity = ContainerManager.Resolve(genericRepoEntityType);

                var method = repoEntity.GetType().GetMethod("Init");

                method.Invoke(repoEntity, null);
            }
        }
        
        public async Task ResetDB()
        {
            var assembly = typeof(IRepository<>).GetTypeInfo().Assembly;
            var entities = assembly.DefinedTypes
                                   .Where(x => (x.IsSubclassOf(typeof(PersistebleEntity)) || x.IsSubclassOf(typeof(LocalEntity)))
                                          && !x.IsAbstract
                                          && !Attribute.IsDefined(x,typeof(IgnoreDbReset)));

            foreach (var entity in entities)
            {
                var genericRepoEntityType = typeof(IRepository<>).MakeGenericType(entity.AsType());
                var repoEntity = ContainerManager.Resolve(genericRepoEntityType);

                var method = repoEntity.GetType().GetMethod("DeleteAll");

                Task result = (Task)method.Invoke(repoEntity, null);
                await result;
            }
        }

        public async Task ResetDB(IList<Type> typesToResert)
        {
            foreach (var entity in typesToResert)
            {
                var genericRepoEntityType = typeof(IRepository<>).MakeGenericType(entity);
                var repoEntity = ContainerManager.Resolve(genericRepoEntityType);

                var method = repoEntity.GetType().GetMethod("DeleteAll");

                Task result = (Task)method.Invoke(repoEntity, null);
                await result;
            }
        }
    }
}
