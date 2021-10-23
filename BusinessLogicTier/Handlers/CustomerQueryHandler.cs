using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataTier.IServices;
using DataTier.Models;
using DataTier.Queries;
using MediatR;

namespace BusinessTier.Handlers
{
    public class CustomerQueryHandler :
        IRequestHandler<GetCustomerById, Customer>,
        IRequestHandler<GetCustomersByHasMortgage, IEnumerable<Customer>>
    {
        private readonly ICustomerService _service; 
        public CustomerQueryHandler(ICustomerService service)
        {
            _service = service;
        }

        public async Task<Customer> Handle(GetCustomerById request, CancellationToken cancellationToken)
        {
            return await _service.GetById(request.CustomerId);
        }

        public async Task<IEnumerable<Customer>> Handle(GetCustomersByHasMortgage request, CancellationToken cancellationToken)
        {
            return await _service.FindBy(request.HasMortgageOffer);
        }
    }
}
