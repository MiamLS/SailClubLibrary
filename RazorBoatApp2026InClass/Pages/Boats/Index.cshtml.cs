using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Reflection;

namespace RazorBoatApp2026InClass.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBoatRepositoryAsync bRepo;
        public List<Boat> Boats { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }
        public IndexModel(IBoatRepositoryAsync boatRepository)
        {
            bRepo = boatRepository;
        }
        public async Task OnGet() //Når vi submitter/trykker på knappen kommer vi ned i OnGet
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Boats = await bRepo.FilterBoatsAsync(FilterCriteria);
            }
            else
            {
                Boats = await bRepo.GetAllBoatsAsync();
            }
            SortBoats();
        }

        private void SortBoats()
        {
            if(!string.IsNullOrEmpty(SortBy))
            {
                if (SortBy == "Id")
                {
                    Boats.Sort();
                }
                else if (SortBy == "SailNumber")
                {
                    Boats.Sort(new BoatCompareBySailNumber());
                }
                else if (SortBy == "YearOfConstruction")
                {
                    Boats.Sort(new BoatCompareByYear());
                }
            }
        }
    }
}
