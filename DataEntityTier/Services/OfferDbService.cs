using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Services
{
    public class OfferDbService : IDbService<MortgageOffer>
    {
        public Task<MortgageOffer> AddAsync(MortgageOffer item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MortgageOffer> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MortgageOffer>> GetMultipleAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task<MortgageOffer> UpdateAsync(MortgageOffer item)
        {
            throw new NotImplementedException();
        }
    }
}
