using DataTier.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataTier.Queries
{
    public class GetCustomersByHasMortgage : IRequest<IEnumerable<Customer>>
    {
        public Expression<Func<Customer, bool>> HasMortgageOffer = (c) => c.MortgageOffer != null;
    }
}
