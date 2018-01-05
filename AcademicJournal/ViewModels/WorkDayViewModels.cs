using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class WorkDaysDetailsVM
    {
        public WorkDay WorkDay { get; set; }
        public Attendance AttendanceModel { get; set; }
    }
}