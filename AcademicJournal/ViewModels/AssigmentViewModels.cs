using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class AssignmentsIndexViewModel
    {
        public IEnumerable<Assignment> Assignments { get; set; }
        public Assignment AssignmentModel { get; set; }
    }
    public class CreateAssigmentVM
    {        
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
    }
    public class CreateAndAssignToSingleUserVM
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
    }
    public class AssignmentEdtiViewModel
    {
        [Required]
        [MaxLength(60)]
        public string Title { get; set; }
        public int AssignmentId { get; set; }
    }

    public class AssignmentDetailsViewModel
    {
        public Assignment Assignment { get; set; }
    }

    public class CreateGroupAssignmentVM
    {
        public IEnumerable<Student> Students { get; set; }
        public CreateAssigmentVM Assignment { get; set; }
        public Student Student { get; set; }
        public HttpPostedFileBase file { get; set; }
        public List<string> studentId { get; set; }
    }

    public class StudentsAndSubmissionsListVM
    {
        public IEnumerable<Submission> Submissions { get; set; }
        public Student StudentModel { get; set; }
        public Assignment Assignment { get; set; }
    }

    public class AssignToStudentsVM
    {
        public IEnumerable<Student> Students { get; set; }
        public Student StudentModel { get; set; }
        public Assignment Assignment { get; set; }
    }

    public class AssignToStudentVM
    {
        public Student Student { get; set; }
        public Assignment AssignmentModel { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
    }

    public class AssignmentsRemoveStudentVM
    {
        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
    }

    public class MentorAssignmentsVM
    {
        public Mentor Mentor { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public Assignment AssignmentModel { get; set; }
    }

    public class DeleteAssigmentViewModel
    {
        public Assignment Assignment { get; set; }
    }
}