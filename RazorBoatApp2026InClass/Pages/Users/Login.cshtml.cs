using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }

        public string Message { get; set; }

        private IUserServiceAsync _userService;
        public LoginModel(IUserServiceAsync userservice)
        {
            _userService = userservice;
        }

        public void OnGet()
        {
        }
        public void OnGetLogout()
        {
            HttpContext.Session.Remove("Username");
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                User loginUser = await _userService.VerifyUserAsync(Username, Password);
                if (loginUser != null)
                {
                    HttpContext.Session.SetString("Username", loginUser.Username);
                    return RedirectToPage("/index");
                }
                else
                {
                    Message = "Invalid username or password";
                    Username = "";
                    Password = "";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("/Users/Login");
        }
    }

}
