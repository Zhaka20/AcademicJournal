using System;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class AttendanceViewModel
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public virtual StudentViewModel Student{ get; set; }

        public int WorkDayId { get; set; }
        public virtual WorkDayViewModel Day { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Come { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Left { get; set; }
    }
}
