using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class SubmissionViewModel
    {
        [DisplayFormat(DataFormatString = "{0}", NullDisplayText = " - ")]
        public byte? Grade { get; set; }
        public bool Completed { get; set; }

        public DateTime? DueDate { get; set; }
        public DateTime? Submitted { get; set; }

        public virtual SubmitFileViewModel SubmitFile { get; set; }

        public string StudentId { get; set; }      
        public virtual StudentViewModel Student { get; set; }

        public int AssignmentId { get; set; }  
        public virtual AssignmentViewModel Assignment { get; set; }

        public virtual IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
