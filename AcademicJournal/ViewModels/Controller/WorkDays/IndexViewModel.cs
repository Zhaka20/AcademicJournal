using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.WorkDays
{
    public class IndexViewModel
    {
        public IEnumerable<WorkDayDTO> WorkDays { get; set; }
        public WorkDay WorkDayDTO { get; set; }
    }
}