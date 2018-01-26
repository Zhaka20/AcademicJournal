using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class StudentsAndSubmissionsListViewModel
    {
        public IEnumerable<Submission> Submissions { get; set; }
        public Student StudentModel { get; set; }
        public Assignment Assignment { get; set; }
    }

}