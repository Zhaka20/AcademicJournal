using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.WorkDays
{
    public class WorkDayDeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int JournalId { get; set; }
    }
}