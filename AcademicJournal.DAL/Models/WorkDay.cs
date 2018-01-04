using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class WorkDay
    {
        public WorkDay()
        {
            Attendances = new List<Attendance>();
        }
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int JournalId { get; set; }
        [ForeignKey("JournalId")]
        public Journal Journal { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
