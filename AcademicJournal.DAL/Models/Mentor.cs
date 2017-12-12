using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Mentor : ApplicationUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(30)]
        public string LastName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
