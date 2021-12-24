using System.Threading.Tasks;

namespace BusinessTier.IHandler
{
    public interface IQueryHandler<IQuery, TResponse>
    {
        public Task<TResponse> Handle(IQuery query);
    }
}
