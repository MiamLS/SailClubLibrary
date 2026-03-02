using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {

        private IBoatRepository _repo;

        private IMemberRepository _mrepo;

        private IBookingRepository _brepo;

        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public Boat BookedBoat { get; set; }

        [BindProperty]
        public string Destination { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        public CreateBookingModel(IBoatRepository boatRepository, IMemberRepository memberRepository, IBookingRepository bookingRepository)
        {
            _repo = boatRepository;
            _mrepo = memberRepository;
            _brepo = bookingRepository;
        }
        public IActionResult OnGet(string sailNumber)
        {
            BookedBoat = _repo.SearchBoat(sailNumber);
            return Page();
        }

        public IActionResult OnPostBook()
        {
            Member member = _mrepo.SearchMember(PhoneNumber);

            Booking newBooking = new Booking(Id, StartDate, EndDate, Destination, member, BookedBoat);

            _brepo.AddBooking(newBooking);

            return RedirectToPage("Index");
        }
    }
}
