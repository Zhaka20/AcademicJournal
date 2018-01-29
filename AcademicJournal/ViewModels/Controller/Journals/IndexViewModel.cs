using AcademicJournal.ViewModels.Controller.Assignments;
using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Journals
{
    public class IndexViewModel
    {
        public IEnumerable<JournalViewModel> Journals { get; set; }
        public Shared.EntityViewModels.MentorViewModel MentorModel { get; set; }
        public JournalViewModel JournalModel { get; set; }
    }
}