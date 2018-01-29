using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class MentorsListViewModel
    {
        public IEnumerable<MentorViewModel> Mentors { get; set; }
        public MentorViewModel MentorVM { get; set; }
    }
}