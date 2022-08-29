
namespace Frontend.WebApi.Common
{
    public interface IMapper<T,Y>
    {
        T Map(Y dto);

        Y Map(T entity);
    }
}
