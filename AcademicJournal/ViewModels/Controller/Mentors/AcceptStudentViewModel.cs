using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class AcceptStudentViewModel
    {
        public IEnumerable<StudentViewModel> Students { get; set; }
        public Students.ShowViewModel StudentVM { get; set; }
    }

}