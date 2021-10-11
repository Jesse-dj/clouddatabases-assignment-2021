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
    public class GetEstatesByPriceRangeHandler : IRequestHandler<GetEstatesByPriceRangeQuery, IEnumerable<Estate>>
    {
        private readonly IRepository<Estate> _repositry;
        public GetEstatesByPriceRangeHandler(IRepository<Estate> repositry)
        {
            _repositry = repositry;
        }
        public async Task<IEnumerable<Estate>> Handle(GetEstatesByPriceRangeQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Estate, object>> isPriceBetweenRange = e => request.priceRange.IsPriceBetween(e.Price);
            var result = await _repositry.AllIncludingAsync(isPriceBetweenRange);
            return result;
        }
    }
}
