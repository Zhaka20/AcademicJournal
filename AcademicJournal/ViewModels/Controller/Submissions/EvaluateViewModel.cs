using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class EvaluateViewModel
    {
        public SubmissionViewModel Submission { get; set; }
        [Range(1, 5)]
        [Required]
        public int Grade { get; set; }
        public int assignmentId { get; set; }
        public string studentId { get; set; }
    }
}