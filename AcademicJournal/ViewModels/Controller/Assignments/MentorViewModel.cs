using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class MentorViewModel
    {
        public MentorViewModel Mentor { get; set; }
        public IEnumerable<AssignmentViewModel> Assignments { get; set; }
        public AssignmentViewModel AssignmentModel { get; set; }
    }
}