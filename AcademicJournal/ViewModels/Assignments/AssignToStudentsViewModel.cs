using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Assignments
{
    public class AssignToStudentsViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public Student StudentModel { get; set; }
        public Assignment Assignment { get; set; }
    }
}