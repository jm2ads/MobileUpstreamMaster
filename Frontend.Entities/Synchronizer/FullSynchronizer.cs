using Frontend.Business.Commons;
using Frontend.Business.IRestServices;
using Frontend.Commons.Commons.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Frontend.Business.Synchronizer
{
    public class FullSynchronizer<T> where T : SyncEntity
    {
        protected readonly IRepository<T> repository;

        protected readonly ISyncRestService<T> restService;

        public FullSynchronizer(IRepository<T> repository, ISyncRestService<T> restService)
        {
            this.restService = restService;
            this.repository = repository;
        }

        public virtual async Task Sync()
        {
            await Upload();
            await Download();
        }

        public async Task Upload()
        {
            // se puede reemplazar por aquellos q estan pendientes U otra regla de negocio
            // q pasa si falla la subida, como lo manejo?
            var entities = await repository.Where(x => x.SyncState != SyncState.Synchronized);
            foreach (var entity in entities)
            {
                //await restService.DoPost(entity);
                //entity.SyncState = SyncState.Sent;
                //await repository.Update(entity);
            }
        }

        public virtual async Task Download()
        {
            // De dnd saco las urls para poder hacer la sincro, las paso por params o creo alguna
            // estructura de datos que tenga el tipo con la relacion de constantes
            var entities = await restService.DoGet(String.Empty);
            await repository.InsertAll(entities);
        }

        public virtual async Task Rollback()
        {
            await this.repository.DropTableAsync();
        }

        public async Task Download(IList<T> entities)
        {
            var entitiesUpdated = entities.Map(UpdateSync);
            await repository.InsertAll(entitiesUpdated);
        }

        private T UpdateSync(T entity)
        {
            entity.Downloaded = DateTime.Now;

            return entity;
        }
    }
}
