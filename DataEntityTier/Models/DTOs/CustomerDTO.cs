using System;

namespace DataTier.Models
{
    public class CustomerDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public double IncomePerYear { get; set; }
    }
}
