using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Mentors
{
    public class MentorAcceptStudentVM
    {
        public IEnumerable<Student> Students { get; set; }
        public ShowStudentVM StudentVM { get; set; }
    }

}