using DependencyInjection.Interface;
namespace DependencyInjection.Services
{
    public class WeekendMeeting:IGreeter
    {
        public string GetMessage()
    {
        return "Weekend meeting will be at 11:00 pm on saturday";
    }
    }
}
