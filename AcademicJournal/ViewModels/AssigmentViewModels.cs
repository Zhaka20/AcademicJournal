using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class CreateAssigmentVM
    {        
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Students Email")]
        public string StudentsEmail { get; set; }
    }
}