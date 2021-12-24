using BusinessTier.IServices;
using DataTier.Models;

namespace BusinessTier.Services
{
    public class MortgageService : IMortgageService
    {
        public MortgageOffer CalculateMortgage(double yearlyIncome)
        {
            const int MONTHS = 12;
            const int YEARS = 30;
            const double RESIDENTIALQUOTE = 0.29;

            double MaximumYearlyMortgageCost = yearlyIncome * RESIDENTIALQUOTE;
            double MaximumMonthlyMortgageCost = MaximumYearlyMortgageCost / MONTHS;
            double MaximumTotalMortgage = MaximumYearlyMortgageCost * YEARS;

            return new MortgageOffer(MaximumTotalMortgage, MaximumMonthlyMortgageCost);
        }
    }
}
