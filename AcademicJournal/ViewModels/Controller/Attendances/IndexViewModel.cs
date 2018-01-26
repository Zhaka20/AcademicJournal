using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Attendances
{
    public class IndexViewModel
    {
        public IEnumerable<Attendance> Attendances { get; set; }
        public Student StudentModel { get; set; }
        public Attendance AttendanceModel { get; set; }
    }
}