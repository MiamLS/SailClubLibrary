using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Threading.Tasks;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        //Vi vil have stillet vores IBoatRepository til rådighed. Det gør vi ved at lave et instancefield, en property BoatToUpdate og en constructor
        private IMemberRepositoryAsync _mrepo;

        [BindProperty] //Når vi har fyldt vores BoatToUpdate op med vores nye data fra formularen, ryger vi ned i vores OnPost metode. Hvis vi skal have vores nye data med tilbage, skal vi tilføje en BindProperty til vores property BoatToUpdate

        public Member MemberToUpdate { get; set; }

        public EditMemberModel(IMemberRepositoryAsync memberRepository)
        {
            _mrepo = memberRepository;
        }
        public async Task OnGet(string phoneNumber)
        {
            MemberToUpdate = await _mrepo.SearchMemberAsync(phoneNumber);
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            await _mrepo.UpdateMemberAsync(MemberToUpdate);
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _mrepo.RemoveMemberAsync(MemberToUpdate);
            return RedirectToPage("Index");
        }
    }
}
