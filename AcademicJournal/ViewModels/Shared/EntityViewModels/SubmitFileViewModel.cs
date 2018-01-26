using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class SubmitFileViewModel : FileInfoViewModel
    {
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
        public virtual SubmissionViewModel Submission { get; set; }
    }
}
