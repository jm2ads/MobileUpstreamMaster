using Frontend.Business.Commons;
using Frontend.Commons.Commons;
using Frontend.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Frontend.Data.Commons
{
    public class Repository<T> : IRepository<T> where T : PersistebleEntity, new()
    {
        protected readonly Database<T> database;

        public Repository(Database<T> database)
        {
            this.database = database;
        }

        public void Init()
        {
            database.CreateTable();
        }

        public virtual async Task<IList<T>> GetAllWithChildren()
        {
            return await database.GetAllWithChildren();
        }

        public virtual async Task<IList<T>> GetAll()
        {
            return await database.GetAll();
        }

        public virtual async Task Save(T entity)
        {
            await database.SaveAsync(entity);
        }

        public async Task Update(T entity)
        {
            await database.Update(entity);
        }

        public async Task DeleteAll()
        {
            await database.DeleteAll();
        }

        public async Task UpdateWithChildren(T entity)
        {
            await database.UpdateWithChildren(entity);
        }

        public async Task<IList<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return await database.Where(predicate);
        }

        public async Task DropTableAsync()
        {
            await database.DropTable();
        }

        public async Task Delete(T entity)
        {
            await database.DeleteAsync(entity);
        }

        public async Task Delete(int entityId)
        {
            await database.DeleteAsync(entityId);
        }

        public async Task InsertAll(IList<T> entities)
        {
            await database.InsertRangeAsync(entities);
        }

        public async Task InsertAllWithChildren(IList<T> entities, bool recursive = false)
        {
            await database.InsertRangeWithChildrenAsync(entities, recursive);
        }


        public async Task UpdateAll(IList<T> entities)
        {
            await database.UpdateRangeAsync(entities);
        }

        public async Task<T> First()
        {
            return await database.First();
        }

        public async Task<T> FirstWithChildren()
        {
            var entity = await database.First();
            if (entity == null)
            {
                return null;
            }
            return await database.GetWithChildren(entity.Id);
        }

        public async Task<T> First(Expression<Func<T, bool>> predicate)
        {
            return await database.Find(predicate);
        }

        public async Task<T> FirstWithChildren(Expression<Func<T, bool>> predicate)
        {
            var results = await database.FindWithChildren(predicate);
            return results.FirstOrDefault();
        }

        public async Task SaveRange(IList<T> newEntities)
        {
            foreach (var newEntity in newEntities)
            {
                await database.SaveAsync(newEntity);
            }
        }

        public async Task DeleteRange(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                await database.DeleteAsync(entity);
            }
        }

        public async Task<IList<T>> FindWithChildren(Expression<Func<T, bool>> predicate, bool recursive = true)
        {
            return await database.FindWithChildren(predicate);
        }

        public async Task<T> FindFirstWithChildren(Expression<Func<T, bool>> predicate)
        {
            var list = await database.FindWithChildren(predicate);
            return list.FirstOrDefault();
        }

        public async Task<T> SaveWithChildren(T entity)
        {
            await database.SaveWithChildren(entity);

            return entity;
        }

        public async Task SaveAllWithChildren(IList<T> entities)
        {
            await database.SaveAllWithChildren(entities);
        }

        public async Task<T> GetWithChildren(int id)
        {
            return await database.GetWithChildren(id);
        }

        public async Task<IList<T>> GetAllByIds(IList<int> idList)
        {
            var retList = new List<T>();
            var tamanioPagina = ApplicationConstants.MaxVariableSqLite;
            for (int i = 0; i <= (idList.Count / tamanioPagina); i++)
            {
                var list = idList.Skip(i * tamanioPagina).Take(tamanioPagina);
                retList.AddRange((await database.Where(x => list.Contains(x.Id))));
            }
            return retList;
        }
    }
}