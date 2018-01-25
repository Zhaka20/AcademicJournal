using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Journals
{
    public class FillViewModel
    {
        public Journal Journal { get; set; }
        public WorkDay WorkDayModel { get; set; }
    }
}