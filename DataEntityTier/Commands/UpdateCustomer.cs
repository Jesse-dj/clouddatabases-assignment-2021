using DataTier.Models;
using MediatR;

namespace DataTier.Commands
{
    public class UpdateCustomer : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
