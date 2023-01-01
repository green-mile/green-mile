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
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The <see cref="Task"/> result contains a <see cref="Result{T}"/>
    /// object with the value <c>true</c> if the notification was
    /// successfully sent, or <c>false</c> otherwise.
    /// </returns>
    public Task<Result<bool>> SendNotification();
    /// <summary>
    /// Sends a notification to a specific user.
    /// </summary>
    /// <param name="user">The user who will receive the notification.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The <see cref="Task"/> result contains a <see cref="Result{T}"/>
    /// object with the value <c>true</c> if the notification was
    /// successfully sent, or <c>false</c> otherwise.
    /// </returns>
    public Task<Result<bool>> SendNotification(User user);
    /// <summary>
    /// Sends a notification to multiple users.
    /// </summary>
    /// <param name="users">The users who will receive the notification.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The <see cref="Task"/> result contains a <see cref="Result{T}"/>
    /// object with the value <c>true</c> if the notification was
    /// successfully sent to all users, or <c>false</c> otherwise.
    /// </returns>
    public Task<Result<bool>> SendNotification(IEnumerable<User> users);
}