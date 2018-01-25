using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Students
{
    public class StudentsIndexViewModel
    {
        public ShowStudentViewModel StudentModel { get; set; }
        public IEnumerable<ShowStudentViewModel> Students { get; set; }
    }
}