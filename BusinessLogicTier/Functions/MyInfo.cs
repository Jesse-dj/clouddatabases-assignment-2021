using Microsoft.Azure.Functions.Worker;

namespace CalculateMortgageAndSendMail.Functions
{
    public class MyInfo
    {
        public ScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }
}
