using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class AssignToStudentViewModel
    {
        public Student Student { get; set; }
        public Assignment AssignmentModel { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
    }
}