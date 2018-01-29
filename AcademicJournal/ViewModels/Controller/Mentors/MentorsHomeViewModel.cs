using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class MentorsHomeViewModel
    {
        public MentorViewModel Mentor { get; set; }
        public JournalViewModel JournalVM { get; set; }
    }
}