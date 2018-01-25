using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Submissions
{
    public class AssignmentSumbissionsViewModel
    {
        public Assignment Assignment { get; set; }
        public IEnumerable<Submission> Submissions { get; set; }
        public Submission SubmissionModel { get; set; }
        public Student StudentModel { get; set; }
    }
}