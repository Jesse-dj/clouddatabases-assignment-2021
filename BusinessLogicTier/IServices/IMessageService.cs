using DataTier.Models;
using System.Threading.Tasks;

namespace BusinessTier.IServices
{
    public interface IMessageService
    {
        public Task SendMessage(string receiver, MortgageOffer offer);
    }
}
