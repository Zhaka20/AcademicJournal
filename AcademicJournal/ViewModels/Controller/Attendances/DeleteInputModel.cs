using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Shared.EntityViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels.Controller.Attendances
{
    public class DeleteInputModel
    {
        [Required]
        public int Id { get; set; }
        public AttendanceViewModel Attendance { get; set; }
    }
}