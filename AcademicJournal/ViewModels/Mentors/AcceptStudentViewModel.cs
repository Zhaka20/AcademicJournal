using AcademicJournal.DataModel.Models;
using System.Collections.Generic;

namespace AcademicJournal.ViewModels.Mentors
{
    public class AcceptStudentViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public Students.ShowViewModel StudentVM { get; set; }
    }

}