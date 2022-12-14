using Microsoft.EntityFrameworkCore;

using Web.Data;
using Web.Models;

namespace Web.Services;

/// <summary>
/// Service for sending notifications to users.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly DataContext _context;

    public NotificationService(DataContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetNotifications(User user)
    {
        return user is null
            ? throw new ArgumentNullException(nameof(user))
            : await _context.Notifications
                .Where(n => n.User == user)
                .ToListAsync();
    }

    public async Task SendNotification(Notification notification)
    {
        if (notification is null)
        {
            throw new ArgumentNullException(nameof(notification));
        }
        var users = _context.Users.AsEnumerable();
        foreach (var user in users)
        {
            var notificationClone = new Notification()
            {
                Message = notification.Message,
                System = notification.System,
                Date = notification.Date,
                Read = notification.Read,
            };
            notificationClone.SetUser(user);
            _context.Notifications.Add(notificationClone);
        }
        await _context.SaveChangesAsync();
    }

    public async Task SendNotification(Notification notification, User user)
    {
        if (notification is null)
        {
            throw new ArgumentNullException(nameof(notification));
        }
        if (user is null)
        {
            throw new ArgumentNullException(nameof(user));
        }
        notification.SetUser(user);
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync();
    }

    public async Task SendNotification(Notification notification, IEnumerable<User> users)
    {
        if (notification is null)
        {
            throw new ArgumentNullException(nameof(notification));
        }
        if (users is null)
        {
            throw new ArgumentNullException(nameof(users));
        }
        foreach (var user in users)
        {
            var notificationClone = new Notification()
            {
                Message = notification.Message,
                System = notification.System,
                Date = notification.Date,
                Read = notification.Read,
            };
            notificationClone.SetUser(user);
            _context.Notifications.Add(notificationClone);
        }
        await _context.SaveChangesAsync();
    }

    public async Task SendNotification(Notification notification, Household household)
    {
        if (notification is null)
        {
            throw new ArgumentNullException(nameof(notification));
        }
        if (household is null)
        {
            throw new ArgumentNullException(nameof(household));
        }
        if (household.Users is null)
        {
            throw new InvalidOperationException($"Member `Users` is `null` in argument {nameof(household)}.");
        }
        foreach (var user in household.Users)
        {
            var notificationClone = new Notification()
            {
                Message = notification.Message,
                System = notification.System,
                Date = notification.Date,
                Read = notification.Read,
            };
            notificationClone.SetUser(user);
            _context.Notifications.Add(notificationClone);
        }
        await _context.SaveChangesAsync();
    }
}