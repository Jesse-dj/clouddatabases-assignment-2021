using DataTier.Interfaces;

namespace DataTier.Commands
{
    public class AddNewCustomer : ICommand
    {
        public string Firstname { get; }
        public string Lastname { get; }
        public string Email { get; }
        public double YearlyIncome { get; }

        public AddNewCustomer(string firstname, string lastname, string email, double yearlyIncome)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            YearlyIncome = yearlyIncome;
        }
    }
}
