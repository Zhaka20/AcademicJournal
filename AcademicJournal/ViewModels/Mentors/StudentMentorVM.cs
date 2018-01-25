using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Mentors
{
    public class StudentMentorVM
    {
        public Student Student { get; set; }
        public Assignment AssignmentModel { get; set; }
        public Submission SubmissionModel { get; set; }
    }
}