using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Models
{
    public class Customer
    {
        public string id { get; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        private DateTime dateOfBirth;
        public DateTime DateOfBirth
        {
            get
            {
                return dateOfBirth.Date;
            }
            set
            {
                if (value.Date >= DateTime.Today) return;
                dateOfBirth = value;
            }
        }
        public double IncomePerYear { get; set; }
        //public double IncomePerMonth => IncomePerYear / 12d;
        public MortgageOffer MortgageOffer { get; set; }
        public bool hasMortgageOffer => MortgageOffer != null;

    }
}
