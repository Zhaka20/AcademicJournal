using System;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class EditViewModel
    {
        [Required]
        public int AssignmentId { get; set; }
        [Required]
        public string StudentId { get; set; }
        [DisplayFormat(DataFormatString = "{0}", NullDisplayText = " - ")]
        [Range(1, 5)]
        [Required]
        public int Grade { get; set; }
        public bool Completed { get; set; }
        [Required]
        public DateTime? DueDate { get; set; }

    }
}