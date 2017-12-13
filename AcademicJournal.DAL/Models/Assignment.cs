using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
    
        public string Title { get; set; }
        public string Description { get; set; }
        public string UploadFile { get; set; }
        public bool Completed { get; set; }
        public byte? Grade { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Submitted { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual Mentor Mentor { get; set; }
        public virtual Student Student { get; set; }
    }
}
