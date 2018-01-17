using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
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
        public int Year { get; set; }
        public string MentorId { get; set; }
        public int Id { get; set; }
    }
    public class DeleteJournalVM
    {
        public Journal Journal { get; set; }
    }
}