using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        //Vi vil have stillet vores IBoatRepository til rådighed. Det gør vi ved at lave et instancefield, en property BoatToUpdate og en constructor
        private IMemberRepository _mrepo;

        [BindProperty] //Når vi har fyldt vores BoatToUpdate op med vores nye data fra formularen, ryger vi ned i vores OnPost metode. Hvis vi skal have vores nye data med tilbage, skal vi tilføje en BindProperty til vores property BoatToUpdate

        public Member MemberToUpdate { get; set; }

        public EditMemberModel(IMemberRepository memberRepository)
        {
            _mrepo = memberRepository;
        }
        public void OnGet(string phoneNumber)
        {
            MemberToUpdate = _mrepo.SearchMember(phoneNumber);
        }
        public IActionResult OnPostUpdate()
        {
            _mrepo.UpdateMember(MemberToUpdate);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            _mrepo.RemoveMember(MemberToUpdate);
            return RedirectToPage("Index");
        }
    }
}
