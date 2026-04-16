using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Users
{
    public class CreateUserModel : PageModel
    {
        private IUserServiceAsync _userService;

        [BindProperty]
        public User NewUser { get; set; }

        public CreateUserModel(IUserServiceAsync userService)
        {
            _userService = userService;

        }
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            //NB bør checke om der er en user allerede med samme username!

            try
            {
                await _userService.AddUserAsync(NewUser);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("/index");
        }
    }


}
