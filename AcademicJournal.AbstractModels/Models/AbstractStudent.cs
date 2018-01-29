using AcademicJournal.DAL.Models;
using System.Collections.Generic;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public abstract class AbstractStudentModel : ApplicationUser
    {
        public string  MentorId { get; set; }
        public virtual AbstractMentorModel Mentor { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
