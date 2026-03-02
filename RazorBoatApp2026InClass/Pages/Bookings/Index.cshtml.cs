using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using SailClubLibrary.Services;

namespace RazorBoatApp2026InClass.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private IBookingRepository _bRepo;

        public List<Booking> Bookings { get; set; }

        public IndexModel(IBookingRepository bookingRepository)
        {
            _bRepo = bookingRepository;
        }
        public void OnGet()
        {
            Bookings = _bRepo.GetAllBookings();
        }
    }
}
