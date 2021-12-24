using DataTier.Interfaces;

namespace DataTier.Queries
{
    public class FindCustomerById : IQuery
    {
        public string CustomerId { get; set; }

        public FindCustomerById(string customerId)
        {
            CustomerId = customerId;
        }
    }
}
