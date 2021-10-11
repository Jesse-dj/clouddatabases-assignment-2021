using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Models
{
    public class Customer : ICustomer
    {
        public uint Id { get; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double IncomePerYear { get; set; }
        //public double IncomePerMonth => IncomePerYear / 12d;
        public MortgageOffer MortgageOffer { get; }
        public bool hasMortgageOffer => MortgageOffer != null;

    }
}
