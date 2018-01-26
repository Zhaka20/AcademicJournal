using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Journals
{
    public class EditViewModel
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public int Id { get; set; }
    }
}