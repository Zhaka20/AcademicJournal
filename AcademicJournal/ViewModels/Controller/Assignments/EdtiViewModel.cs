using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class EdtiViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
        public int AssignmentId { get; set; }
    }
}