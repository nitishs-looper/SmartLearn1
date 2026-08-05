using System.Collections.Generic;

namespace SmartLearn1
{
    // Interface for notification-capable users (Student, Instructor)
    public interface INotifiable
    {
        void SendNotification(string message);
        List<string> GetNotificationHistory();
    }
}
