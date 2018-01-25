using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.WorkDays
{
    public class WorkDayIndexViewModel
    {
        public IEnumerable<WorkDay> WorkDays { get; set; }
        public WorkDay WorkDayModel { get; set; }
    }
}