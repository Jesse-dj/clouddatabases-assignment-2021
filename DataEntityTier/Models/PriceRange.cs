using System;

namespace DataTier
{
    public class PriceRange
    {
        private float startPrice = 0f;
        private float endPrice = float.MaxValue;
        public float StartPrice {
            get
            {
                return startPrice;
            }
            set
            {
                if (value < 0f) startPrice = 0f;
            }
        }
        public float EndPrice { 
            get
            {
                return endPrice;
            }
            set
            {
                endPrice = value;
            }
        }

        public PriceRange(float startPrice, float endprice)
        {
            if (startPrice > endprice)
            {
                float tempDouble = startPrice;
                startPrice = endprice;
                endprice = tempDouble;
            }
            StartPrice = startPrice;
            EndPrice = endprice;
        }

        public bool IsPriceBetween(float price)
        {
            return StartPrice <= price && price <= EndPrice;
        }
    }
}
