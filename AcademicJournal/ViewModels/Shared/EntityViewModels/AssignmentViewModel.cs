using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class AssignmentViewModel
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }

        public int? AssignmentFileId { get; set; }
        public virtual AssignmentFileViewModel AssignmentFile { get; set; }

        public string CreatorId { get; set; }
        public virtual MentorViewModel Creator { get; set; }

        public DateTime Created { get; set; }
        public virtual IEnumerable<SubmissionViewModel> Submissions { get; set; }
    }
}
