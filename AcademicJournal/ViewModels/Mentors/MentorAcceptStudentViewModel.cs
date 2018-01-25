using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Mentors
{
    public class MentorAcceptStudentViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public ShowStudentViewModel StudentVM { get; set; }
    }

}