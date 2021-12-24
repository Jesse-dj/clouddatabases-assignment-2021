namespace DataTier.Commands
{
    public class CustomerReceivedMessage
    {
        public string CustomerId;

        public CustomerReceivedMessage(string customerId)
        {
            CustomerId = customerId;
        }
    }
}