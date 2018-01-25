using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Students
{
    public class IndexViewModel
    {
        public ShowViewModel StudentModel { get; set; }
        public IEnumerable<ShowViewModel> Students { get; set; }
    }
}