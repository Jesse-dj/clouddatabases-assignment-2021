using MediatR;
using Online_Store_API;
using System.Collections.Generic;

namespace DataTier.Queries
{
    public class GetEstatesByPriceRangeQuery : IRequest<IEnumerable<Estate>>
    {
        public PriceRange priceRange { get; set; }
    }
}
