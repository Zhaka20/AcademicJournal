using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Submission
    {
        [DisplayFormat(DataFormatString = "{0}", NullDisplayText = " - ")]
        public byte? Grade { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Submitted { get; set; }

        public virtual SubmitFile SubmitFile { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }

        public int AssignmentId { get; set; }
        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
