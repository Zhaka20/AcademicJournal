﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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