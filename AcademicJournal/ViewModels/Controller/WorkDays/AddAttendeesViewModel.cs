using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.WorkDays
{
    public class AddAttendeesViewModel
    {
        public Student StudentModel { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}