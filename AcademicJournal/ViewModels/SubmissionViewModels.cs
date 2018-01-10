using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{

    public class EvaluateSubmissionVM
    {
        public Submission Submission { get; set; }
        [Range(1, 100)]
        [Required]
        public int Grade { get; set; }
    }

    public class EvaluateSubmissionInputModel
    {
        [Range(1, 100)]
        [Required]
        public int Grade { get; set; }
    }
}