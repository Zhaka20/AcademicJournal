using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
   public class AttendanceIndexViewModel
    {
        public IEnumerable<Attendance> Attendances { get; set; }
        public Student StudentModel { get; set; }
        public Attendance AttendanceModel { get; set; }
    }
    public class AttendancesDetailsViewModel
    {
        public Attendance Attendance { get; set; }
    }
    public class EditAttendanceViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Come { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Left { get; set; }
    }
    public class DeleteAttendanceViewModel
    {
        public Attendance Attendance { get; set; }
    }
    public class DeleteAttendanceInputModel
    {
        [Required]
        public int Id { get; set; }
        public Attendance Attendance { get; set; }
    }
}