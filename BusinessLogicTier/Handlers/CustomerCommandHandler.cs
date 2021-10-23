using AutoMapper;
using DataTier.Commands;
using DataTier.IServices;
using DataTier.Models;
using DataTier.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessTier.Handlers
{
    public class CustomerCommandHandler :
        IRequestHandler<AddNewCustomer, Customer>
    {
        private ICustomerService _customerDbService;
        private IMapper _mapper;

        public CustomerCommandHandler(ICustomerService customerDbService)
        {
            _customerDbService = customerDbService;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CustomerDTO, Customer>();
            });
            _mapper = config.CreateMapper();
        }

        async Task<Customer> IRequestHandler<AddNewCustomer, Customer>.Handle(AddNewCustomer request, CancellationToken cancellationToken)
        {
            Customer newCustomer = _mapper.Map<CustomerDTO, Customer>(request.Customer);
            return await _customerDbService.Add(newCustomer);
        }
    }
}
