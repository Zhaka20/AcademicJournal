using System;
using System.Collections.Generic;
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
        public byte? Grade { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Submitted { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskFile TaskFile { get; set; }
        public TaskFile SubmitFile { get; set; }

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public virtual Mentor Creator { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
