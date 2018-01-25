using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Students
{
    public class StudentsHomeViewModel
    {
        public Student Student { get; set; }
        public Assignment AssignmentModel { get; set; }
        public Submission SubmissionModel { get; set; }
        public IEnumerable<Submission> Submissions { get; set; }
    }
}