using Web.Data;
using Web.Models;
using Web.Utils;

namespace Web.Services;

/// <summary>
/// Service for sending notifications to users.
/// </summary>
public class NotificationService : INotificationService
{
    public Task<bool> SendNotification(Notification notification)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendNotification(Notification notification, User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SendNotification(Notification notification, IEnumerable<User> users)
    {
        throw new NotImplementedException();
    }
}