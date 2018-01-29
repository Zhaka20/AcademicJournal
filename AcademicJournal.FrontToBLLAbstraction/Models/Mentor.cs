using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class Mentor : ApplicationUser
    {
        public ICollection<Student> Students { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

        public ICollection<Journal> Journals { get; set; }
    }
}
