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

        public CreateWorkDayViewModel GetCreateWorkDayViewModelAsync()
        {
            return new CreateWorkDayViewModel();
        }

        public Task CreateJournalAsync(CreateJournalVM viewModel)
        {
            throw new NotImplementedException();
        }

        

        public Task DeleteJournalAsync(int journalId)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<CreateJournalVM> GetCreateJournalViewModelAsync()
        {
            throw new NotImplementedException();
        }

      

        public Task<DeleteJournalVM> GetDeleteJournalViewModelAsync(int journalId)
        {
            throw new NotImplementedException();
        }

        public Task<EditJournalVM> GetEditJournalViewModelAsync(int journalId)
        {
            throw new NotImplementedException();
        }

        public Task<JournalDetailVM> GetJournalDetailsViewModelAsync(int journalId)
        {
            throw new NotImplementedException();
        }

        
        public Task<JournalIndexViewModel> GetJournalsIndexViewModelAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateJournalAsync(EditJournalVM viewModel)
        {
            throw new NotImplementedException();
        }
    }
}