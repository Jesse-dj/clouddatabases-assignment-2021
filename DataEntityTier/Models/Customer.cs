using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataTier.Models
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public double IncomePerYear { get; set; }
        public MortgageOffer? MortgageOffer { get; set; }
        [JsonProperty(PropertyName = "partitonKey")]
        public string partitionKey { get; set; } = Guid.NewGuid().ToString();
    }
}
