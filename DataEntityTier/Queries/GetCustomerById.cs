using DataTier.Models;
using MediatR;

namespace DataTier.Queries
{
    public class GetCustomerById : IRequest<Customer>
    {
        public string CustomerId { get; set; }
    }
}
