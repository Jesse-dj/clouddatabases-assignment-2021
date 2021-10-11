using DataTier;

namespace Online_Store_API
{
    public class Estate
    {
        public uint Id { get; set; }
        public Address Address { get; set; }
        public float Price { get; set; }
        public ListingStatus Status { get; set; }

    }
}
