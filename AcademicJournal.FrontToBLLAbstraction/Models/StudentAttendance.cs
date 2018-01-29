using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class StudentAttendance
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public DateTime? Came { get; set; }
        public DateTime? Left { get; set; }
    }
}
