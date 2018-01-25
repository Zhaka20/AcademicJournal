using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.WorkDays
{
    public class DetailsViewModel
    {
        public WorkDay WorkDay { get; set; }
        public Attendance AttendanceModel { get; set; }
    }
}