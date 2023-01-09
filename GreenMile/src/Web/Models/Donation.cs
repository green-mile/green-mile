using System.ComponentModel.DataAnnotations;

namespace Web.Models;

/// <summary>
/// Represents a donation offer that a user create.
/// </summary>
public class Donation
{
    /// <summary>
    /// The unique identifier for the donation offer.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The status of the donation offer, whether pending, active, complete, expired.
    /// </summary>
    public DonationStatus Status { get; set; }

    /// <summary>
    /// The date and time that the donation offer was created.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The user that created the donation offer.
    /// </summary>

    public User? User { get; set; }

    /// <summary>
    /// Refer to the food item class
    /// </summary>
    public FoodItem? FoodItem { get; set; }
}

public enum DonationStatus
{
    PENDING,
    ACTIVE,
    COMPLETE,
    EXPIRED
}