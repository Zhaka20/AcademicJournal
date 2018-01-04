using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public virtual ICollection<WorkDay> WorkDays{ get; set; }

        public string MentorId { get; set; }
        [ForeignKey("MentorId")]
        public virtual Mentor Mentor { get; set; }
    }
}
