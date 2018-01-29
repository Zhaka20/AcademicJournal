using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.WEB.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public ICollection<WorkDay> WorkDays{ get; set; }

        public string MentorId { get; set; }
        public virtual Mentor Mentor { get; set; }
    }
}
