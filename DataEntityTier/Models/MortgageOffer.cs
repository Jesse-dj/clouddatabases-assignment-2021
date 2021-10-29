using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataTier
{
    [Microsoft.EntityFrameworkCore.Owned]
    public class MortgageOffer
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// Total mortgage the customer can receive. 
        /// </summary>
        public double TotalMortgage { get; set; }
        /// <summary>
        /// Monthly payment the customer can pay per month.
        /// </summary>
        public double MonthlyPayments { get; set; }
        public double Annuity { get; set; } = 0.064419;
        public DateTime Created { get; set; }
        public string CustomerId { get; set; }
        public MortgageOffer() { }

        public MortgageOffer(double yearlyIncome)
        {
            MonthlyPayments = yearlyIncome * 0.29 / 12;
            TotalMortgage = yearlyIncome * 0.29 / Annuity;
            Created = DateTime.Now;
        }
    }
}
