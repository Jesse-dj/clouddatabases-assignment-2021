using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier
{
    public class MortgageOffer
    {
        public Guid Id { get; } = Guid.NewGuid();
        public uint CustomerId { get; }
        public uint Years { get; } = 30;
        /// <summary>
        /// Total mortgage the customer can get. 
        /// </summary>
        public double TotalMortgage { get; }
        /// <summary>
        /// Monthly payment the customer can pay per month.
        /// </summary>
        public double MonthlyPayments { get; }
        public double Annuity { get; } = 0.064419;
        public MortgageOffer(double yearlyIncome)
        {
            MonthlyPayments = yearlyIncome * 0.29 / 12;
            TotalMortgage = yearlyIncome * 0.29 / Annuity;
        }
    }
}
