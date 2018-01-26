using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class MentorViewModel : ApplicationUserViewModel
    {
        public virtual IEnumerable<StudentViewModel> Students { get; set; }
        public virtual IEnumerable<AssignmentViewModel> Assignments { get; set; }

        public virtual IEnumerable<JournalViewModel> Journals { get; set; }
    }
}
