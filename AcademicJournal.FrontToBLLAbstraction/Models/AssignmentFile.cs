using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class AssignmentFile : FileInfo
    {
        public ICollection<Assignment> Assignments { get; set; }
    }
}
