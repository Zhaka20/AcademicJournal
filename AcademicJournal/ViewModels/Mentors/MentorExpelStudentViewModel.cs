using AcademicJournal.ViewModels.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Mentors
{
    public class MentorExpelStudentViewModel
    {
        public ShowStudentViewModel Student { get; set; }
        public string MentorId { get; set; }
    }
}