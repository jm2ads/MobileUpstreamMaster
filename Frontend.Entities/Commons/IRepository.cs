using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Frontend.Business.Commons
{
    public interface IRepository<T> where T : class 
    {
        void Init();
        Task<IList<T>> GetAllWithChildren();
        Task<IList<T>> GetAll();
        Task Save(T entity);
        Task Update(T entity);
        Task<T> First();
        Task<T> First(Expression<Func<T, bool>> predicate);
        Task<T> FirstWithChildren();
        Task<T> FirstWithChildren(Expression<Func<T, bool>> predicate);
        Task SaveRange(IList<T> list);
        Task DeleteAll();
        Task UpdateWithChildren(T entity);
        Task<IList<T>> Where(Expression<Func<T, bool>> predicate);
        Task DropTableAsync();
        Task Delete(T entity);
        Task Delete(int entityId);
        Task InsertAll(IList<T> entities);
        Task InsertAllWithChildren(IList<T> entities, bool recursive = false);
        Task UpdateAll(IList<T> entities);
        Task<IList<T>> FindWithChildren(Expression<Func<T, bool>> predicate, bool recursive = true);
        Task<T> FindFirstWithChildren(Expression<Func<T, bool>> predicate);
        Task<T> SaveWithChildren(T entity);
        Task SaveAllWithChildren(IList<T> entities);
        Task<T> GetWithChildren(int id);
        Task<IList<T>> GetAllByIds(IList<int> idList);
    }
}
