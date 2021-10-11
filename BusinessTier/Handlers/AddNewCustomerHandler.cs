using DataTier.Commands;
using DataTier.Models;
using DataTier.Repositories;
using MediatR;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Queue; // Namespace for Queue storage types
using Microsoft.Azure.Storage;
using Microsoft.Azure;
using System;
using DataTier;
using DataTier.Services;
using AutoMapper;

namespace Online_Store_API.Handlers
{
    public class AddNewCustomerHandler : IRequestHandler<AddNewCustomerCommand, Customer>
    {
        private readonly IRepository<Customer> _repository;
        private IQueueStorage _queuestorage;
        private CustomerDbService _customerDbService;
        private IMapper _mapper;

        public AddNewCustomerHandler(IRepository<Customer> repository, IQueueStorage queueStorage, CustomerDbService customerDbService)
        {
            _repository = repository;
            _queuestorage = queueStorage;
            _customerDbService = customerDbService;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Customer, CustomerDTO>();
            });
            _mapper = config.CreateMapper();
        }

        public async Task<Customer> Handle(AddNewCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer newCustomer = _mapper.Map<CustomerDTO, Customer>(request.Customer);
            var response = await _customerDbService.AddAsync(newCustomer);

            var message = new CloudQueueMessage(response.id.ToString());
            var queue = await _queuestorage.GetQueueAsync("customerinfo");
            await queue.AddMessageAsync(message);

            return response;
        }
    }
}
