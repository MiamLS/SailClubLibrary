using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using SailClubLibrary.Services;
using System.Threading.Tasks;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class DeleteMemberModel : PageModel
    {

        private IMemberRepositoryAsync _repo;

        public Member DeleteMember { get; set; }

        public DeleteMemberModel(IMemberRepositoryAsync memberRepository)
        {
            _repo = memberRepository;
        }
        public async Task<IActionResult> OnGet(string phoneNumber)
        {
            DeleteMember = await _repo.SearchMemberAsync(phoneNumber);
            return Page();
        }

        public async Task<IActionResult> OnPostDelete(Member member) //IActionResult redirecter os til en return page, i dette tilfælde vores index side i vores Boats folder
        {
            await _repo.RemoveMemberAsync(member);
            return RedirectToPage("Index");
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Index");
        }
    }
}
