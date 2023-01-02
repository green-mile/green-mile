using Web.Models;
using Web.Utils;

namespace Web.Services;

/// <summary>
/// An interface for a notification service that allows users to events and other important information.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Sends a notification to all users.
    /// </summary>
    /// <param name="notification">The notification the users will receive.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The <see cref="Task"/> result contains a <see cref="Result{T}"/>
    /// object with the value <c>true</c> if the notification was
    /// successfully sent, or <c>false</c> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="notification"/> is <c>null</c>.
    /// </exception>
    public Task<Result<bool>> SendNotification(Notification notification);
    /// <summary>
    /// Sends a notification to a specific user.
    /// </summary>
    /// <param name="notification">The notification the user will receive.</param>
    /// <param name="user">The user who will receive the notification.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The <see cref="Task"/> result contains a <see cref="Result{T}"/>
    /// object with the value <c>true</c> if the notification was
    /// successfully sent, or <c>false</c> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="notification"/> or <paramref name="user"/> is <c>null</c>.
    /// </exception>
    public Task<Result<bool>> SendNotification(Notification notification, User user);
    /// <summary>
    /// Sends a notification to multiple users.
    /// </summary>
    /// <param name="notification">The notification the users will receive.</param>
    /// <param name="users">The users who will receive the notification.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The <see cref="Task"/> result contains a <see cref="Result{T}"/>
    /// object with the value <c>true</c> if the notification was
    /// successfully sent to all users, or <c>false</c> otherwise.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="notification"/> or <paramref name="users"/> is <c>null</c>.
    /// </exception>
    public Task<Result<bool>> SendNotification(Notification notification, IEnumerable<User> users);
}