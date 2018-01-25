using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Mentors
{
    public class MentorsListVM
    {
        public IEnumerable<Mentor> Mentors { get; set; }
        public ShowMentorVM MentorVM { get; set; }
    }
}