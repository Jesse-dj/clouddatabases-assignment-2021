using DataTier.Models;

namespace DataTier.Commands
{
    public class AddMortgageOffer
    {
        public string customerId { get; set; }
        public MortgageOffer mortgageOffer { get; set; }

        public AddMortgageOffer(string customerId, MortgageOffer mortgageOffer)
        {
            this.customerId = customerId;
            this.mortgageOffer = mortgageOffer;
        }
    }
}
