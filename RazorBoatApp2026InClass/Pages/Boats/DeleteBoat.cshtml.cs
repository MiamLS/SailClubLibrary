using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class DeleteBoatModel : PageModel
    {
        //Vi vil have adgang til den båd, som skal slettes. Det gør vi ved at lave en public property Boat, som vi kalder DeleteBoat, vi laver et private instancefield af typen IBoatRepository som vi kalder _repo (det har vi adgang til gennem vores services i program.cs) og vi laver en constructor DeleteBoatModel, som lægger vores parameteroverførte boatRepository over i _repo 
        private IBoatRepositoryAsync _repo;
        
        public Boat DeleteBoat { get; set; }

        public DeleteBoatModel(IBoatRepositoryAsync boatRepository)
        {
            _repo = boatRepository;
        }
        public async Task<IActionResult> OnGet(string sailNumber)
        {
            DeleteBoat = await _repo.SearchBoatAsync(sailNumber);
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(string sailNumber) //IActionResult redirecter os til en return page, i dette tilfælde vores index side i vores Boats folder
        {
            await _repo.RemoveBoatAsync(sailNumber);
            return RedirectToPage("Index");
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
