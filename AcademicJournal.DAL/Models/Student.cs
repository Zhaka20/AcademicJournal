using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Student : ApplicationUser
    {
        public string  MentorId { get; set; }
        [ForeignKey("MentorId")]
        public virtual Mentor Mentor { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}
