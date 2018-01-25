using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Assignments
{
    public class IndexViewModel
    {
        public IEnumerable<Assignment> Assignments { get; set; }
        public Assignment AssignmentModel { get; set; }
    }
}