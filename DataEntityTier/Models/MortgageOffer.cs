using System;

namespace DataTier.Models
{
    public class MortgageOffer
    {
        public double TotalMortgage { get; set; }
        public double MonthlyPayments { get; set; }
        public DateTime Created { get; set; }
        public Customer Customer { get; set; }

        public MortgageOffer()
        {

        }
        public MortgageOffer(double totalMortgage, double monthlyPayments)
        {
            TotalMortgage = totalMortgage;
            MonthlyPayments = monthlyPayments;
            Created = DateTime.Now;
        }
    }
}
