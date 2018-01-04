using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class JournalDetailVM
    {
        public WorkDay WorkDay { get; set; }
        public Attendance AttendanceModel { get; set; }
    }

    public class JournalFillVM
    {
        public Journal Journal { get; set; }
        public WorkDay WorkDayModel { get; set; }
    }
}