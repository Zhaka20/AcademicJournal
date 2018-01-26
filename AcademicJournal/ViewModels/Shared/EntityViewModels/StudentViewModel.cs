using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class StudentViewModel : ApplicationUserViewModel
    {
        public string  MentorId { get; set; }
        public virtual MentorViewModel Mentor { get; set; }

        public virtual IEnumerable<AttendanceViewModel> Attendances { get; set; }
        public virtual IEnumerable<SubmissionViewModel> Submissions { get; set; }
    }
}
