using AcademicJournal.ViewModels.Shared.EntityViewModels;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class ExpelStudentViewModel
    {
        public StudentViewModel Student { get; set; }
        public string MentorId { get; set; }
    }
}