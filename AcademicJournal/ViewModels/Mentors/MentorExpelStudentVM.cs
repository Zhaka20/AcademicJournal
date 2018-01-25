using AcademicJournal.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Mentors
{
    public class MentorExpelStudentVM
    {
        public ShowStudentVM Student { get; set; }
        public string MentorId { get; set; }
    }
}