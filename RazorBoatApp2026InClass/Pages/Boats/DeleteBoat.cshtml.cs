using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class DeleteBoatModel : PageModel
    {
        //Vi vil have adgang til den båd, som skal slettes. Det gør vi ved at lave en public property Boat, som vi kalder DeleteBoat, vi laver et private instancefield af typen IBoatRepository som vi kalder _repo (det har vi adgang til gennem vores services i program.cs) og vi laver en constructor DeleteBoatModel, som lægger vores parameteroverførte boatRepository over i _repo 
        private IBoatRepository _repo;
        
        public Boat DeleteBoat { get; set; }

        public DeleteBoatModel(IBoatRepository boatRepository)
        {
            _repo = boatRepository;
        }
        public IActionResult OnGet(string sailNumber)
        {
            DeleteBoat = _repo.SearchBoat(sailNumber);
            return Page();
        }

        public IActionResult OnPostDelete(string sailNumber) //IActionResult redirecter os til en return page, i dette tilfælde vores index side i vores Boats folder
        {
            _repo.RemoveBoat(sailNumber);
            return RedirectToPage("Index");
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
