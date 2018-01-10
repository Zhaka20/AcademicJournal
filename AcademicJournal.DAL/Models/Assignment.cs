using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        
        public string Title { get; set; }
        public bool Completed { get; set; }
        [DisplayFormat(DataFormatString = "{0}",NullDisplayText = " - ")]
        public byte? Grade { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Submitted { get; set; }
        public DateTime? DueDate { get; set; }

        public int? TaskFileId { get; set; }
        public virtual TaskFile TaskFile { get; set; }

        public int? SubmitFileId { get; set; }
        public virtual TaskFile SubmitFile { get; set; }

        public string CreatorId { get; set; }
        public virtual Mentor Creator { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
