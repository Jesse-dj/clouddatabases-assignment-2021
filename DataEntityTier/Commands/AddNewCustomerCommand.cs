using DataTier.Models;
using MediatR;

namespace DataTier.Commands
{
    public class AddNewCustomerCommand : IRequest<Customer>
    {
        public CustomerDTO Customer { get; set; }
    }
}
