using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTier.Models
{
    public class Customer
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public double IncomePerYear { get; set; }
        public MortgageOffer MortgageOffer { get; set; }
    }
}
