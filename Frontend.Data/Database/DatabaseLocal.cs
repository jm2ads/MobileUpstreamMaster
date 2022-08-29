using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using Frontend.Business.Commons;

namespace Frontend.Data.Database
{
    public class DatabaseLocal<T> where T : LocalEntity, new()
    {
        private readonly SQLiteAsyncConnection connection;

        public DatabaseLocal(SQLiteAsyncConnection connection)
        {
            this.connection = connection;
            CreateTable();
        }

        public async void CreateTable()
        {
            await connection.CreateTableAsync<T>();
        }

        public async Task DropTable()
        {
            await connection.DropTableAsync<T>();
        }

        public async Task<IList<T>> GetAll()
        {
            return await connection.GetAllWithChildrenAsync<T>(recursive: true);
        }

        public async Task<int> SaveAsync(T entity)
        {
            return await this.connection.InsertOrReplaceAsync(entity);
        }

        public async Task<IList<T>> Where(Expression<Func<T, bool>> predicate)
        {
            return await connection.Table<T>().Where(predicate).ToListAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            await connection.DeleteAsync(entity, recursive: true);
        }

        public async Task Update(T entity)
        {
            await connection.UpdateAsync(entity);
        }

        public async Task DeleteAll()
        {
            await this.connection.DropTableAsync<T>();
            await this.connection.CreateTableAsync<T>();
        }

        public async Task InsertRangeWithChildrenAsync(IList<T> entities)
        {
            await connection.InsertAllWithChildrenAsync(entities, recursive: true);
        }

        public async Task InsertRangeAsync(IList<T> entities)
        {
            await connection.InsertAllAsync(entities);
        }

        public async Task UpdateRangeAsync(IList<T> entities)
        {
            await connection.UpdateAllAsync(entities);
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await this.connection.Table<T>().Where(predicate).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Devuelve el primero null si no existe
        /// </summary>
        /// <returns></returns>
        public async Task<T> First()
        {
            var aux = await connection.Table<T>().FirstOrDefaultAsync();
            if (aux == null)
            {
                return null;
            }

            return await connection.GetWithChildrenAsync<T>(aux.Id);
        }

        public async Task UpdateWithChildren(T entity)
        {
            await connection.UpdateWithChildrenAsync(entity);
        }

        public async Task<IList<T>> FindWithChildren(Expression<Func<T, bool>> predicate)
        {
            return await connection.GetAllWithChildrenAsync<T>(predicate, recursive: true);
        }

        public async Task SaveWithChildren(T entity)
        {
            await connection.InsertWithChildrenAsync(entity, recursive: true);
        }

        /// <summary>
        /// Soporte de string queries
        /// </summary>
        /// <param name="querySintax"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<IList<T>> Query(string querySintax, params object[] args)
        {
            return await connection.QueryAsync<T>(querySintax, args);
        }

        public async Task<T> GetWithChildren(int id)
        {
            return await connection.GetWithChildrenAsync<T>(id, recursive: true);
        }
    }
}
