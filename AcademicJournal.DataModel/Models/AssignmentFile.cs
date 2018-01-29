using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DataModel.Models
{
    public class AssignmentFile : FileInfo
    {
        public virtual ICollection<Assignment> Assignments { get; set; }
    }
}
