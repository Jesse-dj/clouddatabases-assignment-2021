using MediatR;
using Online_Store_API;
using System.Collections.Generic;

namespace DataTier.Queries
{
    public class GetEstatesQuery : IRequest<IEnumerable<Estate>>
    {
        public string searchTerm { get; set; }
        public PriceRange priceRange { get; set; }
    }
}
