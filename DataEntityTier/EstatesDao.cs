using Online_Store_API;
using System;
using System.Collections.Generic;

namespace DataTier
{
    public interface EstatesDao
    {
        Estate FindById(uint estateId);
        IEnumerable<Estate> FindByPriceRange(PriceRange priceRange);
    }
}
