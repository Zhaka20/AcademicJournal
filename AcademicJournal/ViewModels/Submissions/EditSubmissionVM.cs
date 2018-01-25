﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Submissions
{
    public class EditSubmissionVM
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