using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using AcademicJournal.ViewModels;
using System.Threading.Tasks;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;

namespace AcademicJournal.Services.ControllerServices
{
    public class JournalsControllerService : IJournalsControllerService
    {
        private ApplicationDbContext db;

        public JournalsControllerService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<JournalFillVM> GetJournalFillViewModelAsync(int journalId)
        {
            Journal journal = await db.Journals.Include(a => a.WorkDays).
                                                Include(b => b.Mentor).
                                                FirstOrDefaultAsync(j => j.Id == journalId);
            if (journal == null)
            {
                return null;
            }
            JournalFillVM viewModel = new JournalFillVM
            {
                Journal = journal,
                WorkDayModel = new WorkDay()
            };

            return viewModel;
        }

        public CreateWorkDayViewModel GetCreateWorkDayViewModel(int journalId)
        {
            CreateWorkDayViewModel viewModel = new CreateWorkDayViewModel
            {
                Day = DateTime.Now,
                JournalId = journalId
            };
            return viewModel;
        }

        public async Task CreateWorkDayAsync(CreateWorkDayViewModel viewModel)
        {
            var journal = await db.Journals.FindAsync(viewModel.JournalId);
            WorkDay newWorkDay = new WorkDay
            {
                JournalId = viewModel.JournalId,
                Day = viewModel.Day
            };
            journal.WorkDays.Add(newWorkDay);
            await db.SaveChangesAsync();
        }

        public async Task<JournalIndexViewModel> GetJournalsIndexViewModelAsync()
        {
            var journals = await db.Journals.Include(j => j.Mentor).ToListAsync();
            JournalIndexViewModel viewModel = new JournalIndexViewModel
            {
                Journals = journals,
                JournalModel = new Journal(),
                MentorModel = new Mentor()
            };
            return viewModel;
        }

        public async Task<JournalDetailVM> GetJournalDetailsViewModelAsync(int journalId)
        {
            Journal journal = await db.Journals.Include(j => j.Mentor).
                                                FirstOrDefaultAsync(j => j.Id == journalId);
            if (journal == null)
            {
                return null;
            }
            JournalDetailVM viewModel = new JournalDetailVM
            {
                Journal = journal
            };
            return viewModel;
        }

        public CreateJournalVM GetCreateJournalViewModel(string mentorId)
        {
            Journal journal = new Journal
            {
                MentorId = mentorId,
                Year = DateTime.Now.Year
            };

            CreateJournalVM viewModel = new CreateJournalVM
            {
                MentorId = journal.MentorId,
                Year = journal.Year
            };
            return viewModel;
        }
        public async Task<int> CreateJournalAsync(CreateJournalVM viewModel)
        {
            Journal newJournal = new Journal
            {
                Year = viewModel.Year,
                MentorId = viewModel.MentorId,
            };

            db.Journals.Add(newJournal);
            await db.SaveChangesAsync();
            return newJournal.Id;
        }

        public async Task<EditJournalVM> GetEditJournalViewModelAsync(int journalId)
        {
            Journal journal = await db.Journals.
                                       Include(j => j.Mentor).
                                       FirstOrDefaultAsync(j => j.Id == journalId);
            if (journal == null)
            {
                return null;
            }

            EditJournalVM viewModel = new EditJournalVM
            {
                Year = journal.Year,
                Id = journal.Id
            };
            return viewModel;
        }

        public async Task UpdateJournalAsync(EditJournalVM viewModel)
        {
            Journal updatedJournal = new Journal
            {
                Id = viewModel.Id,
                Year = viewModel.Year,
            };
            db.Journals.Attach(updatedJournal);
            db.Entry(updatedJournal).Property(j => j.Year).IsModified = true;
            await db.SaveChangesAsync();
        }

        public async Task<DeleteJournalVM> GetDeleteJournalViewModelAsync(int journalId)
        {
            Journal journal = await db.Journals.FindAsync(journalId);
            if (journal == null)
            {
                return null;
            }
            DeleteJournalVM viewModel = new DeleteJournalVM
            {
                Journal = journal
            };
            return viewModel;
        }

        public async Task DeleteJournalAsync(int journalId)
        {
            Journal journal = await db.Journals.FindAsync(journalId);
            db.Journals.Remove(journal);
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}