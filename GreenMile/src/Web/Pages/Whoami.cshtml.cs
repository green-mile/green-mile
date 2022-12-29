using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Lib;

namespace Web.Pages;

public class WhoamiModel : PageModel
{
    private readonly IHttpContextAccessor _http;

    public WhoamiModel(IHttpContextAccessor http)
    {
        _http = http;
    }

    public string UserName { get; set; }
    public string UserId { get; set; }

    public void OnGet()
    {
        UserName = _http.HttpContext.Session.GetString(SessionVariable.UserName);
        UserId = _http.HttpContext.Session.GetString(SessionVariable.UserId);
    }
}