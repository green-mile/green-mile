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
    /// would be <c>Food Sharing</c>
    /// </summary>
    public string System { get; set; } = string.Empty;

    /// <summary>
    /// The date and time that the notification was created.
    /// </summary>
    public DateTime Date { get; set; }
}
