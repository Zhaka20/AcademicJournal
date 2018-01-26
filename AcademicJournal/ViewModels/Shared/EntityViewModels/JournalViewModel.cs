using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class JournalViewModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public IEnumerable<WorkDayViewModel> WorkDays{ get; set; }

        public string MentorId { get; set; }
        public virtual MentorViewModel Mentor { get; set; }
    }
}
