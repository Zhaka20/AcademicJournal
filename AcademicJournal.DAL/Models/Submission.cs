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

        [ForeignKey("Student")]
        [Key, Column(Order = 1)]
        public string StudentId { get; set; }      
        public virtual Student Student { get; set; }

        [ForeignKey("Assignment")]
        [Key, Column(Order = 0)]
        public int AssignmentId { get; set; }  
        public virtual Assignment Assignment { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
