using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Mentors
{
    public class MentorsListViewModel
    {
        public IEnumerable<Mentor> Mentors { get; set; }
        public ShowViewModel MentorVM { get; set; }
    }
}