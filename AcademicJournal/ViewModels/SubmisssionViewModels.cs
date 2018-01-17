using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class AssignmentSumbissionsVM
    {
        public Assignment Assignment { get; set; }
        public IEnumerable<Submission> Submissions { get; set; }
        public Submission SubmissionModel { get; set; }
        public Student StudentModel { get; set; }
    }

    public class EvaluateSubmissionVM
    {
        public Submission Submission { get; set; }
        [Range(1, 5)]
        [Required]
        public int Grade { get; set; }
        public int assignmentId { get; set; }
        public string studentId { get; set; }
    }

    public class EvaluateSubmissionInputModel
    {
        [Range(1, 5)]
        [Required]
        public int Grade { get; set; }
        [Required]
        public int assignmentId { get; set; }
        [Required]
        public string studentId { get; set; }        
    }

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

    public class UploadFileSubmissionVM
    {
        public Assignment Assignment { get; set; }
    }

    public class SubmissionsIndexVM
    {
        public IEnumerable<Submission> Submissions { get; set; }
        public Assignment AssignmentModel { get; set; }
        public Submission SubmissionModel { get; set; }
    }
    public class SubmissionDetailsVM
    {
        public Submission Submission { get; set; }
        public Student StudentModel { get; set; }
        public Assignment AssignmentModel { get; set; }
    }

    public class DeleteSubmissionVM
    {
        public Submission Submission { get; set; }
        public Student StudentModel { get; set; }
        public Assignment AssignmentModel { get; set; }
    }

}