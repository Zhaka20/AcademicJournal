using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DataModel.Models
{
    public class SubmitFile : FileInfo
    {
        [Key, Column(Order = 0)]
        public int AssignmentId { get; set; }
        [Key, Column(Order = 1)]
        public string StudentId { get; set; }
        [ForeignKey("AssignmentId,StudentId")]
        public virtual Submission Submission { get; set; }
    }
}
