using DataTier.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataTier.Queries
{
    public class GetCustomersByNewlyCreated : IRequest<IEnumerable<Customer>>
    {
        public Expression<Func<Customer, bool>> offerMadeWithin24Hours;

        public GetCustomersByNewlyCreated()
        {
            var last24Hours = DateTime.Now.AddHours(-24);
            offerMadeWithin24Hours = c => c.MortgageOffer.Created > last24Hours;
        }
    }
}
