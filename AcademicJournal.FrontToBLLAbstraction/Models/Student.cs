using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class Student : ApplicationUser
    {
        public string  MentorId { get; set; }
        public virtual Mentor Mentor { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Submission> Submissions { get; set; }
    }
}
