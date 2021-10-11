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

namespace Online_Store_API.Handlers
{
    public class AddNewCustomerHandler : IRequestHandler<AddNewCustomerCommand, Customer>
    {
        private readonly IRepository<Customer> _repository;
        private IQueueStorage _queuestorage;

        public AddNewCustomerHandler(IRepository<Customer> repository, IQueueStorage queueStorage)
        {
            _repository = repository;
            _queuestorage = queueStorage;
        }

        public async Task<Customer> Handle(AddNewCustomerCommand request, CancellationToken cancellationToken)
        {
            string serializedObject = JsonConvert.SerializeObject(request.Customer);              
            var message = new CloudQueueMessage(serializedObject);
            var queue = await _queuestorage.CreateQueueAsync("customerinfo");
            await queue.AddMessageAsync(message);

            return await _repository.AddAsync(request.Customer);
        }
    }
}
