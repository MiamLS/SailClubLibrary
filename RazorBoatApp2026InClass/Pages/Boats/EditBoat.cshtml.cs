using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        //Vi vil have stillet vores IBoatRepository til rådighed. Det gør vi ved at lave et instancefield, en property BoatToUpdate og en constructor
        private IBoatRepository _repo;

        [BindProperty] //Når vi har fyldt vores BoatToUpdate op med vores nye data fra formularen, ryger vi ned i vores OnPost metode. Hvis vi skal have vores nye data med tilbage, skal vi tilføje en BindProperty til vores property BoatToUpdate

        public Boat BoatToUpdate { get; set; }

        public EditBoatModel(IBoatRepository boatRepository)
        {
            _repo = boatRepository;
        }
        public void OnGet(string sailNumber)
        {
            BoatToUpdate = _repo.SearchBoat(sailNumber);
        }
        public IActionResult OnPostUpdate()
        {
            _repo.UpdateBoat(BoatToUpdate);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            _repo.RemoveBoat(BoatToUpdate.SailNumber);
            return RedirectToPage("Index");
        }


    }
}
