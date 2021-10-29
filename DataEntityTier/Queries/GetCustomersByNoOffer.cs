using DataTier.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataTier.Queries
{
    public class GetCustomersByNoOffer : IRequest<IEnumerable<Customer>>
    {
        public string HasNoMortgageOfferQuery = "SELECT * FROM c WHERE c.MortgageOffer = null";

        /*public Expression<Func<Customer, bool>> HasNoMortgageOffer = (c) => c.MortgageOffer == null;*/
    }
}
