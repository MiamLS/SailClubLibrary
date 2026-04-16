using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        //Vi vil have stillet vores IBoatRepository til rådighed. Det gør vi ved at lave et instancefield, en property BoatToUpdate og en constructor
        private IBoatRepositoryAsync _repo;

        [BindProperty] //Når vi har fyldt vores BoatToUpdate op med vores nye data fra formularen, ryger vi ned i vores OnPost metode. Hvis vi skal have vores nye data med tilbage, skal vi tilføje en BindProperty til vores property BoatToUpdate

        public Boat BoatToUpdate { get; set; }

        public EditBoatModel(IBoatRepositoryAsync boatRepository)
        {
            _repo = boatRepository;
        }
        public async Task OnGet(string sailNumber)
        {
            BoatToUpdate = await _repo.SearchBoatAsync(sailNumber);
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            await _repo.UpdateBoatAsync(BoatToUpdate);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _repo.RemoveBoatAsync(BoatToUpdate.SailNumber);
            return RedirectToPage("Index");
        }


    }
}
