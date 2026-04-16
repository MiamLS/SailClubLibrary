using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class CreateBoatModel : PageModel
    {
        private IBoatRepositoryAsync _repo;

        [BindProperty]
        public Boat NewBoat { get; set; }

        public CreateBoatModel(IBoatRepositoryAsync boatRepository)
        {
            _repo = boatRepository;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _repo.AddBoatAsync(NewBoat);
            }
            catch (BoatSailnumberExistsException bex)
            {
                ViewData["ErrorMessage"] = bex.Message;
                return Page();
            }
            catch (Exception exp)
            {
                ViewData["ErrorMessage"] = exp.Message;
            }
            return RedirectToPage("Index");
        }
    }
}
