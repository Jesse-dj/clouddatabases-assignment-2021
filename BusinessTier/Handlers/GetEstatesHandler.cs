using DataTier.Queries;
using DataTier.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Online_Store_API.Handlers
{
    public class GetEstatesHandler : IRequestHandler<GetEstatesQuery, IEnumerable<Estate>>
    {
        private readonly IRepository<Estate> _repositry;
        public GetEstatesHandler(IRepository<Estate> repositry)
        {
            _repositry = repositry;
        }
        public async Task<IEnumerable<Estate>> Handle(GetEstatesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Estate, object>> isPriceBetweenRange = e => request.priceRange.IsPriceBetween(e.Price);
            var result = await _repositry.AllIncludingAsync(isPriceBetweenRange);
            return result;
        }
    }
}
