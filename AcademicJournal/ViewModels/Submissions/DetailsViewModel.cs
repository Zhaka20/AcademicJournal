using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Submissions
{
    public class DetailsViewModel
    {
        public Submission Submission { get; set; }
        public Student StudentModel { get; set; }
        public Assignment AssignmentModel { get; set; }
    }
}