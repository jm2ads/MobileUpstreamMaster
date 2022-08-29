using Frontend.Business.Commons;
using Frontend.Business.IData;
using Frontend.Commons.Commons.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Business.Synchronizer
{
    public class LocalSynchronizer<T> where T : SyncLocalEntity
    {
        protected readonly IRepository<T> repository;

        protected readonly ISyncRestService<T> restService;
        private readonly IDatabaseManager databaseManager;

        public LocalSynchronizer(IRepository<T> repository, ISyncRestService<T> restService, IDatabaseManager databaseManager)
        {
            this.restService = restService;
            this.databaseManager = databaseManager;
            this.repository = repository;
        }

        public async Task DropTables()
        {
            try
            {
                await repository.DropTableAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task Sync()
        {
            await Upload();
        }

        public async Task Upload()
        {
            var entities = await repository.FindWithChildren(x => x.SyncState == SyncState.PendingToSync);
            if (entities != null && entities.Count > 0)
            {
                var entitiesSuccess = await restService.DoPost(entities);
                var entitiesSyncronized = entities.Map(SetUploadedSync);
                await repository.UpdateAll(entitiesSyncronized);
                if (entitiesSuccess != null)
                {
                    foreach (var entity in entitiesSuccess)
                    {
                        await repository.Delete(entity);
                    }
                }
            }
        }

        public virtual async Task Rollback()
        {
            await this.repository.DropTableAsync();
        }

        private T SetUploadedSync(T entity)
        {
            entity.Uploaded = DateTime.UtcNow;
            entity.SyncState = SyncState.Synchronized;
            return entity;
        }
    }
}
