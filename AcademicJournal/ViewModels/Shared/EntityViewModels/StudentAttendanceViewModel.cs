using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class StudentAttendanceViewModel
    {
        public int Id { get; set; }
        public StudentViewModel Student { get; set; }
        public DateTime? Came { get; set; }
        public DateTime? Left { get; set; }
    }
}
