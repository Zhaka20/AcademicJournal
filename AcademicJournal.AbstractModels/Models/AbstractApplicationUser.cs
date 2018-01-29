using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AcademicJournal.AbstractModels.Models
{
    public abstract class AbstractApplicationUser : IdentityUser
    {
        virtual public string FirstName { get; set; }
        virtual public string LastName { get; set; }
        virtual public string FullName
        {
            get { return FirstName + "  " + LastName; }
        }

        public virtual ICollection<AbstractComment> Comments { get; set; }
        public abstract Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AbstractApplicationUser> manager);
    }
}
