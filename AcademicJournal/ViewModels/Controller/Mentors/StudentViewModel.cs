using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public Assignment AssignmentModel { get; set; }
        public Submission SubmissionModel { get; set; }
    }
}