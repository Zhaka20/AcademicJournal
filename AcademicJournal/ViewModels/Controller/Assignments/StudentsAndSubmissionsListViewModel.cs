using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class StudentsAndSubmissionsListViewModel
    {
        public IEnumerable<SubmissionViewModel> Submissions { get; set; }
        public StudentViewModel StudentModel { get; set; }
        public AssignmentViewModel Assignment { get; set; }
    }

}