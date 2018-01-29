using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class CreateAndAssignToSingleUserViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        public StudentViewModel Student { get; set; }
    }
}