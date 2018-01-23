using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels
{
    public class WorkDaysDetailsVM
    {
        public WorkDay WorkDay { get; set; }
        public Attendance AttendanceModel { get; set; }
    }

    public class WorkDayCreateViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }
        [Required]
        public int JournalId { get; set; }
    }

    public class WorkDayIndexViewModel
    {
        public IEnumerable<WorkDay> WorkDays { get; set; }
        public WorkDay WorkDayModel { get; set; }
    }

    public class WorkDayEditViewModel
    {
        public WorkDay WorkDay { get; set; }
    }

    public class WorkDayDeleteViewModel
    {
        public WorkDay WorkDay { get; set; }
    }

    public class WorDayAddAttendeesViewModel
    {
        public Student StudentModel { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }

    public class WorkDayDeleteInputModel
    {
        [Required]
        public int Id { get; set; }     
        [Required]
        public int JournalId { get; set; }       
    }
}