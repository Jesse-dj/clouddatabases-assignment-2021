using DataTier.Repositories;
using MediatR;
using Online_Store_API.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Store_API.Handlers
{
    public class GetEstateByIdHandler : IRequestHandler<GetEstateByIdQuery, Estate>
    {
        private readonly IRepository<Estate> _repository;

        public GetEstateByIdHandler(IRepository<Estate> repository)
        {
            _repository = repository;
        }
        public async Task<Estate> Handle(GetEstateByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetSingleAsync(request.Id);
            return result;
        }
    }
}
