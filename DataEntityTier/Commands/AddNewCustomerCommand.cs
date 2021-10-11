using DataTier.Models;
using MediatR;

namespace DataTier.Commands
{
    public class AddNewCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
