using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Assignments
{
    public class RemoveStudentViewModel
    {
        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
    }
}