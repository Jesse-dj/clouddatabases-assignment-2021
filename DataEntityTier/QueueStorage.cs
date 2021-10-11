using System;
using System.Threading.Tasks;
using Microsoft.Azure.Storage; // Namespace for CloudStorageAccount
using Microsoft.Azure.Storage.Queue; // Namespace for Queue storage types

namespace DataTier
{
    public class QueueStorage : IQueueStorage
    {
        private readonly string connectionString;
        //private readonly string queueName;
        private CloudStorageAccount storageAccount;
        private CloudQueueClient queueClient;
        //private CloudQueue queue;

        public QueueStorage()
        {
            this.connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            this.storageAccount = CloudStorageAccount.Parse(connectionString);
            this.queueClient = storageAccount.CreateCloudQueueClient();
        }

        public async Task<CloudQueue> GetQueueAsync(string queueName)
        {
            var cloudQueue = queueClient.GetQueueReference(queueName);
            await cloudQueue.CreateIfNotExistsAsync();

            return cloudQueue;
        }
        public async Task CreateMessageAsync(string Message)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteMessageAsync()
        {
            throw new NotImplementedException();
        }
    }
}
