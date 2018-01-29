using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.Collections.Generic;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class CreateGroupAssignmentViewModel
    {
        public IEnumerable<StudentViewModel> Students { get; set; }
        public CreateViewModel Assignment { get; set; }
        public StudentViewModel Student { get; set; }
        public HttpPostedFileBase file { get; set; }
        public List<string> studentId { get; set; }
    }
}