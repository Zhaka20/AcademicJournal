using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Assignments
{
    public class CreateAndAssignToSingleUserViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }

        public Student Student { get; set; }
    }
}