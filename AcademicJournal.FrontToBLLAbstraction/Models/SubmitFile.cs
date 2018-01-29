using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class SubmitFile : FileInfo
    {
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
        public Submission Submission { get; set; }
    }
}
