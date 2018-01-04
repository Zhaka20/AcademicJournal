using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Student Student{ get; set; }

        public int WorkDayId { get; set; }
        [ForeignKey("WorkDayId")]
        public virtual WorkDay Day { get; set; }

        public DateTime? Come { get; set; }
        public DateTime? Left { get; set; }
    }
}
