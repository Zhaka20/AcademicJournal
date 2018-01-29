using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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