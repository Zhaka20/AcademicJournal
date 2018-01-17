using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class JournalDetailVM
    {
        public Journal Journal { get; set; }     
    }

    public class JournalFillVM
    {
        public Journal Journal { get; set; }
        public WorkDay WorkDayModel { get; set; }
    }

    public class CreateJournalVM
    {
        public int Year { get; set; }
        public string MentorId { get; set; }
    }
    public class EditJournalVM
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public int Id { get; set; }
    }
    public class DeleteJournalVM
    {
        public Journal Journal { get; set; }
    }

    public class JournalIndexViewModel
    {
        public IEnumerable<Journal> Journals { get; set; }
        public Mentor MentorModel { get; set; }
        public Journal JournalModel { get; set; }
    }
}