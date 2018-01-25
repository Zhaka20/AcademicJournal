using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Journals
{
    public class JournalIndexViewModel
    {
        public IEnumerable<Journal> Journals { get; set; }
        public Mentor MentorModel { get; set; }
        public Journal JournalModel { get; set; }
    }
}