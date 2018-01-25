using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Students
{
    public class StudentsIndexViewModel
    {
        public ShowStudentVM StudentModel { get; set; }
        public IEnumerable<ShowStudentVM> Students { get; set; }
    }
}