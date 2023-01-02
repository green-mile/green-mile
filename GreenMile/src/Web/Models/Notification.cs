using System.ComponentModel.DataAnnotations;

namespace Web.Models;

/// <summary>
/// Represents a notification that a user receives.
/// </summary>
public class Notification
{
    /// <summary>
    /// The unique identifier for the notification.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The message of the notification.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The sub-system/module that the notification came from. An example
    /// would be "Food Sharing"
    /// </summary>
    public string System { get; set; } = string.Empty;

    /// <summary>
    /// The date and time that the notification was created.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Indicates whether the notification has been read.
    /// </summary>
    public bool Read { get; set; }

    /// <summary>
    /// The user that will receive the notification.
    /// </summary>
    public User? User { get; private set; }

    /// <summary>
    /// Sets the user that will receive the notification.
    /// </summary>
    /// <param name="user">The user to set.</param>
    public void SetUser(User user)
    {
        User = user;
    }
}
