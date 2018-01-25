using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.WorkDays
{
    public class WorDayAddAttendeesViewModel
    {
        public Student StudentModel { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}