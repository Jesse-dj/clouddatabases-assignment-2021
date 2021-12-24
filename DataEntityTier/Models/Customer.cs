namespace DataTier.Models
{
    public class Customer
    {
        public string CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public double YearlyIncome { get; set; }
        public bool ReceivedMessage { get; set; } = false;
        public MortgageOffer? MortgageOffer { get; set; } = null;
    }
}
