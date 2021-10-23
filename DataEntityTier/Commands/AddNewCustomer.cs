using DataTier.Models;
using MediatR;

namespace DataTier.Commands
{
    public class AddNewCustomer : IRequest<Customer>
    {
        public CustomerDTO Customer { get; set; }
    }
}
