using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class MentorsHomeViewModel
    {
        public Mentor Mentor { get; set; }
        public Journal JournalVM { get; set; }
    }
}