using System.Collections.Generic;

namespace SmartLearn1
{
    public interface INotifiable
    {
        void SendNotification(string message);
        List<string> GetNotificationHistory();
    }
}
