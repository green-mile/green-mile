using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;

namespace Web.Pages;

[Authorize]
public class NotificationsModel : PageModel
{
    private readonly INotificationService _notificationService;
    private readonly UserManager<User> _userManager;

    public NotificationsModel(INotificationService notificationService, UserManager<User> userManager)
    {
        _notificationService = notificationService;
        _userManager = userManager;
    }

    public IEnumerable<Notification>? Notifications { get; set; }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        Notifications = await _notificationService.GetNotifications(user);
    }
}