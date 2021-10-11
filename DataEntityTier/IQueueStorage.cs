using Microsoft.Azure.Storage.Queue;
using System.Threading.Tasks;

namespace DataTier
{
    public interface IQueueStorage
    {
        /// <summary>
        /// Gets a CloudQueue or Creates it if it not exists
        /// </summary>
        /// <param name="queueName"></param>
        /// <returns></returns>
        Task<CloudQueue> GetQueueAsync(string queueName);
        Task CreateMessageAsync(string Message);
        Task DeleteMessageAsync();
    }
}
