using AcademicJournal.ViewModels.Controller.Assignments;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class MentorsListViewModel
    {
        public IEnumerable<MentorViewModel> Mentors { get; set; }
        public ShowViewModel MentorVM { get; set; }
    }
}