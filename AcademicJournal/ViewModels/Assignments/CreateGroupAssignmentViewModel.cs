using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Assignments
{
    public class CreateGroupAssignmentViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public CreateAssigmentViewModel Assignment { get; set; }
        public Student Student { get; set; }
        public HttpPostedFileBase file { get; set; }
        public List<string> studentId { get; set; }
    }
}