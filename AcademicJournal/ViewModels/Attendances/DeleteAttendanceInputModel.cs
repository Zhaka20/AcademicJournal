using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Attendances
{
    public class DeleteAttendanceInputModel
    {
        [Required]
        public int Id { get; set; }
        public Attendance Attendance { get; set; }
    }
}