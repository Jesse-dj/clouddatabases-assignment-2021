namespace DataTier
{
    public struct PriceRange
    {
        public double StartPrice { get; }
        public double EndPrice { get; }

        public PriceRange(double startPrice, double endprice)
        {
            if (startPrice > endprice)
            {
                double tempDouble = startPrice;
                startPrice = endprice;
                endprice = tempDouble;
            }
            StartPrice = startPrice;
            EndPrice = endprice;
        }

        public bool IsPriceBetween(double price)
        {
            return StartPrice <= price && price <= EndPrice;
        }
    }
}
