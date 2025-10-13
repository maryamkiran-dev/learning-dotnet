using DependencyInjection.Interface;
namespace DependencyInjection.Services
{
    public class MeetingMessage : IGreeter
    {
        public string GetMessage()
        {
            return "Todays meeting will be at 4:30pm";

        }
    }
}
 
