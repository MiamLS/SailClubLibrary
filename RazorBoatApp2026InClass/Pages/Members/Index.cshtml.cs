using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Helpers.Sorting;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Globalization;
using System.Threading.Tasks;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepositoryAsync mRepo; //Instance field bRepo til at lægge vores members ind i nede i vores constructor
        public List<Member> Members { get; set; } //Vi vil gerne have en liste af Members stillet til rådighed. Til det skal vi have en auto-property af typen List med vores Members. Når vi får lavet en using til vores sailclub.models, kan vi referere til vores Members med @models

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public MemberType? SelectedMemberType { get; set; }
        public IndexModel(IMemberRepositoryAsync memberRepository) //For at kunne fylde vores liste af members op, laver vi en constructor, hvor vi parameteroverfører det, vi skal bruge
        {
            mRepo = memberRepository;
        }
        //Hvis vi skal have vist vores index side (den foranliggende), så er det første programmet gør, når vi forespørger på index.cshtml, så går den ind i constructoren. Når den har udført den, går den ned og kalder vores OnGet metode (vores controller). Vi bruger OnGet når vi skal vise noget, ellers OnPost hvis vi skal hente noget.
        public async Task OnGet() //Vi vil fylde vores Members op, til det bruger vi vores metode GetAllMembers fra vores SailClubLibrary
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Members = await mRepo.FilterMembersAsync(FilterCriteria);
            }
            else
                Members = await mRepo.GetAllMembersAsync();
            SortMembers();
        }

        public void SortMembers()
        {
            if (!string.IsNullOrEmpty(SortBy))
            {
                if (SortBy == "Id")
                {
                    Members.Sort();
                }
                else if (SortBy == "PhoneNumber")
                {
                    Members.Sort(new MemberCompareByPhoneNumber());
                }
                else if (SortBy == "SurName")
                {
                    Members.Sort(new MemberCompareBySurName());
                }
            }
        }
    }
}
