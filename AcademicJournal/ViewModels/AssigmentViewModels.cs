using AcademicJournal.DAL.Models;
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
    }

    public class EditAssigmentVM
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due date")]
        public DateTime? DueDate { get; set; }
    }

    public class EvaluateAssignmentVM
    {
        public Assignment Assignment { get; set; }
        [Range(1,100)]
        [Required]
        public int Grade { get; set; }
    }

    public class EvaluateAssignmentInputModel
    {
        [Range(1, 100)]
        [Required]
        public int Grade { get; set; }
    }

    public class AssignmentDetailsVM
    {
        public Assignment Assignment { get; set; }
        public Comment Comment { get; set; }
    }

    public class CreateGroupAssignmentVM
    {
        public IEnumerable<Student> Students { get; set; }
        public CreateAssigmentVM Assignment { get; set; }
        public Student Student { get; set; }
        public HttpPostedFileBase file { get; set; }
        public List<string> studentId { get; set; }
    }
}