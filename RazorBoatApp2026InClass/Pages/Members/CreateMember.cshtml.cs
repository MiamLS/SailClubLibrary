using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Threading.Tasks;

namespace RazorBoatApp2026InClass.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        private IMemberRepositoryAsync _mrepo;

        private IWebHostEnvironment webHostEnvironment;

        [BindProperty] //Fordi vi skal hente noget NED i vores property, skal vi bruge en Bindproperty. Vores NewMember kan nu bindes fra det foranliggende og få informationer ind
        public Member NewMember { get; set; } //For at vi kan læse noget ind udefra siden, skal vi lave en public property så vores foranliggende side bliver lagt ind i den bagvedliggende side.

        [BindProperty]
        public IFormFile Photo { get; set; }

        //Constructor
        public CreateMemberModel(IMemberRepositoryAsync memberRepository, IWebHostEnvironment webHost) //Vi injecter vores IMemberRepository og kalder det memberRepository
        {
            _mrepo = memberRepository; //Vi parameteroverfører vores memberRepository ind i vores _mrepo
            webHostEnvironment = webHost;
        }

        public void OnGet() //Bruges når vi skal vise noget, fx vores tomme form
        {
        }


        public async Task<IActionResult> OnPost() //Når vi trykker submit i vores form, bindes vores indtastede info til vores NewMember i vores bagvedliggende side (vi ryger ind i vores constructor og alle vores indtastede info gettes og settes og overføres fra vores foranliggende index til vores bagvedliggende via vores [BindProperty])
        {
            if (Photo != null)
            {
                if (NewMember.MemberImage != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "/images/MemberImages", NewMember.MemberImage);
                    System.IO.File.Delete(filePath); //Hvis der allerede er et foto, slettes det og erstattes
                }

                NewMember.MemberImage = ProcessUploadedFile();

                //    if (!ModelState.IsValid)
                //{
                //    return Page();
                //}
                }
                try
                {
                    await _mrepo.AddMemberAsync(NewMember);
                }
                catch (MemberPhoneNumberExistsException mex)
                {
                    ViewData["ErrorMessage"] = mex.Message;
                    return Page();
                }
                catch (Exception exp)
                {
                    ViewData["ErrorMessage"] = exp.Message;
                    return Page();
                }
                return RedirectToPage("Index");
            }
            //_mrepo.AddMember(NewMember);
            //return RedirectToPage("Index"); //Vi sætter returtypen til IActionResult, så kan vi lave et specielt return statement, som redirecter os til vores index side

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/MemberImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName; //Genererer et unikt ID til vores billede
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
