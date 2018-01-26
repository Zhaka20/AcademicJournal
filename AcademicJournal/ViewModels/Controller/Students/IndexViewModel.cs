using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Students
{
    public class IndexViewModel
    {
        public ShowViewModel StudentModel { get; set; }
        public IEnumerable<ShowViewModel> Students { get; set; }
    }
}