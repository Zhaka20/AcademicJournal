using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class TaskFile
    {
        public int id { get; set; }
        public string UploadFile { get; set; }
        public string FileName { get; set; }

        [ForeignKey("Assignment")]
        public int? AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
