using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class WorkDay
    {
        public WorkDay()
        {
            Attendances = new List<Attendance>();
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        public int JournalId { get; set; }
        public Journal Journal { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}
