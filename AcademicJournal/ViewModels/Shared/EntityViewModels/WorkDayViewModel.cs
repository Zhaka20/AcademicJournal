using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class WorkDayViewModel
    {
        public WorkDayViewModel()
        {
            Attendances = new List<AttendanceViewModel>();
        }
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        public int JournalId { get; set; }

        public JournalViewModel Journal { get; set; }
        public IEnumerable<AttendanceViewModel> Attendances { get; set; }
    }
}
