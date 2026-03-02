using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        private IMemberRepository _mrepo;

        [BindProperty] //Fordi vi skal hente noget NED i vores property, skal vi bruge en Bindproperty. Vores NewMember kan nu bindes fra det foranliggende og få informationer ind
        public Member NewMember { get; set; } //For at vi kan læse noget ind udefra siden, skal vi lave en public property så vores foranliggende side bliver lagt ind i den bagvedliggende side.

        //Constructor
        public CreateMemberModel(IMemberRepository memberRepository) //Vi injecter vores IMemberRepository og kalder det memberRepository
        {
            _mrepo = memberRepository; //Vi parameteroverfører vores memberRepository ind i vores _mrepo
        }

        public void OnGet() //Bruges når vi skal vise noget, fx vores tomme form
        {
        }

        
        public IActionResult OnPost() //Når vi trykker submit i vores form, bindes vores indtastede info til vores NewMember i vores bagvedliggende side (vi ryger ind i vores constructor og alle vores indtastede info gettes og settes og overføres fra vores foranliggende index til vores bagvedliggende via vores [BindProperty])
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                _mrepo.AddMember(NewMember);
            }
            catch (MemberPhoneNumberExistsException mex)
            {
                ViewData["ErrorMessage"] = mex.Message;
                return Page();
            }
            catch (Exception exp)
            {
                ViewData["ErrorMessage"] = exp.Message;
            }
            return RedirectToPage("Index");
        }
            //_mrepo.AddMember(NewMember);
            //return RedirectToPage("Index"); //Vi sætter returtypen til IActionResult, så kan vi lave et specielt return statement, som redirecter os til vores index side
    }
}
