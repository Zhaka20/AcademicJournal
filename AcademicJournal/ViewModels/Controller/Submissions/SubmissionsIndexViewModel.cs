using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Submissions
{
    public class SubmissionsIndexViewModel
    {
        public IEnumerable<Submission> Submissions { get; set; }
        public Assignment AssignmentModel { get; set; }
        public Submission SubmissionModel { get; set; }
    }
}