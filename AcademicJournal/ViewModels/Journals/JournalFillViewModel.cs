using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Journals
{
    public class JournalFillViewModel
    {
        public Journal Journal { get; set; }
        public WorkDay WorkDayModel { get; set; }
    }
}