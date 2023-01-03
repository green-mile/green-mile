using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Web.Models;
using Web.Services;
using Web.UiState;
using Web.Utils;

namespace Web.Pages
{
    [Authorize]
    public class TransferHouseholdModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IHouseholdService _householdService;


        public TransferHouseholdModel(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor contextAccessor, IHouseholdService householdService)
        {
            _householdService = householdService;
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            
        }
        [BindProperty]
        public TransferHouseholdUiState TransferHouseholdUiState { get; set; } = new TransferHouseholdUiState();


        public async Task<IActionResult> OnGetAsync()
        {
            User user = await _userManager.GetUserAsync(HttpContext.User);
            if (!await _userManager.IsInRoleAsync(user, "Member") && user.HouseholdId is null) 
            {
                TempData["info"] = "You may have gotten kicked out of the household! Please join another one";
             
           
            }

            


            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if ((bool)TransferHouseholdUiState.JoinHousehold && TransferHouseholdUiState.InviteLink is null)
            {
                ModelState.AddModelError("TransferHouseholdUiState.JoinHouseholdName", "Please fill in the household's invite code you want to join!");
                return Page();
            }
            else if ((bool)!TransferHouseholdUiState.JoinHousehold && TransferHouseholdUiState.CreateHouseholdName is null)
            {
                ModelState.AddModelError("TransferHouseholdUiState.CreateHouseholdName", "Please fill in the household name you want to create!");
                if (TransferHouseholdUiState.Address is null) ModelState.AddModelError("HouseholdUiState.Address", "Please fill in the address!");

                return Page();
            }


            if (ModelState.IsValid)
            {
                if ((bool)TransferHouseholdUiState.JoinHousehold)
                {

                    Result<Household> householdResult = await _householdService.VerifyLink(TransferHouseholdUiState.InviteLink);
                    if (householdResult.Status == Status.FAILURE)
                    {
                        ModelState.AddModelError("TransferHouseholdUiState.JoinHouseholdName", householdResult.Message);
                        householdResult.Print();
                        return Page();
                    }
                }
                else if ((bool)!TransferHouseholdUiState.JoinHousehold)
                {
                    Result<Household> createHouseholdResult = await _householdService.RetrieveHouseholdDetailsByName(TransferHouseholdUiState.CreateHouseholdName);
                    if (createHouseholdResult.Status == Status.SUCCESS)
                    {
                        ModelState.AddModelError("TransferHouseholdUiState.CreateHouseholdName", createHouseholdResult.Message);
                        createHouseholdResult.Print();
                        return Page();
                    }
                }


                var userId = (await _userManager.GetUserAsync(HttpContext.User)).Id;








                    if ((bool)!TransferHouseholdUiState.JoinHousehold)
                    {
                        await _householdService.CreateHousehold(TransferHouseholdUiState.CreateHouseholdName, TransferHouseholdUiState.Address, userId);
                        await _householdService.AddUserToHousehold(userId, (await _householdService.RetrieveHouseholdDetailsByName(TransferHouseholdUiState.CreateHouseholdName)).Value.HouseholdId);
                        TempData["success"] = "Created household!";

                    }
                    else
                    {

                        await _householdService.AddUserToHousehold(userId, (await _householdService.VerifyLink(TransferHouseholdUiState.InviteLink)).Value.HouseholdId);
                        TempData["success"] = "Transferred household!";
                    }

                    //_contextAccessor.HttpContext.Session.SetString(SessionVariable.UserName, UserName);
                    //_contextAccessor.HttpContext.Session.SetString(SessionVariable.UserId, userId);
                    //_contextAccessor.HttpContext.Session.SetString(SessionVariable.HousholdName, user.Household.Name);

                    return RedirectToPage("/Index");
                }

        
            
            return Page();
        }


    }
}
