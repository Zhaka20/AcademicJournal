using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class WorkDaysDetailsVM
    {
        public WorkDay WorkDay { get; set; }
        public Attendance AttendanceModel { get; set; }
    }

    public class CreateWorkDayViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        public int JournalId { get; set; }
    }

    public class JournalIndexViewModel
    {
        public IEnumerable<Journal> Journals { get; set; }
    }
}