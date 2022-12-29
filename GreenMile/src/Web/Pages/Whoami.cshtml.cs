using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Lib;
using Web.Models;
using Web.Services;

namespace Web.Pages;

public class WhoamiModel : PageModel
{
    private readonly IHttpContextAccessor _http;
    private readonly IHouseholdService _householdService;

    public WhoamiModel(IHttpContextAccessor http, IHouseholdService householdService)
    {
        _http = http;
        _householdService = householdService;
    }

    public string? UserName { get; set; }
    public string? UserId { get; set; }
    public string? HouseholdName { get; set; }
    public User[] HouseholdMembers { get; set; }

    public async Task OnGet()
    {
        UserName = _http.HttpContext?.Session.GetString(SessionVariable.UserName);
        UserId = _http.HttpContext?.Session.GetString(SessionVariable.UserId);
        HouseholdName = _http.HttpContext?.Session.GetString(SessionVariable.HousholdName);
        var result = await _householdService.RetrieveHouseholdDetailsByName(HouseholdName);
        HouseholdMembers = result.Value?.Users.ToArray() ?? Array.Empty<User>();
    }
}