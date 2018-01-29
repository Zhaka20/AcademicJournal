using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(30)]
        public string FullName
        {
            get { return FirstName + "  " + LastName; }
        }

        public ICollection<Comment> Comments { get; set; }      
    }
}
