using DataTier.Models;

namespace BusinessTier.IServices
{
    public interface IMortgageService
    {
        public MortgageOffer CalculateMortgage(double yearlyIncome);
    }
}
