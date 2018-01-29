using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class EvaluateInputModel
    {
        [Range(1, 5)]
        [Required]
        public int Grade { get; set; }
        [Required]
        public int assignmentId { get; set; }
        [Required]
        public string studentId { get; set; }
    }
}