using System.Threading.Tasks;

namespace Frontend.Business.IData
{
    public interface ITransactionalData<T> : IParametricData<T> where T :class 
    {
        Task Save(T entity);

        Task Update(T entity);
    }
}
