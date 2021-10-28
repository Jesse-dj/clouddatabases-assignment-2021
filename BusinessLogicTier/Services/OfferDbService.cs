using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Services
{
    public class OfferDbService : IDbService<MortgageOffer>
    {
        public Task<MortgageOffer> Add(MortgageOffer item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MortgageOffer> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MortgageOffer>> GetMultiple(string query)
        {
            throw new NotImplementedException();
        }

        public Task<MortgageOffer> Update(MortgageOffer item)
        {
            throw new NotImplementedException();
        }
    }
}
