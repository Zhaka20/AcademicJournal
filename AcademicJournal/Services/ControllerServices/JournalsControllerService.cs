using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcademicJournal.DataModel.Models;
using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.ViewModels.Journals;
using AcademicJournal.ViewModels.WorkDays;

namespace AcademicJournal.Services.ControllerServices
{
    public class JournalsControllerService : IJournalsControllerService
    {
        protected readonly IJournalService service;

        public JournalsControllerService(IJournalService service)
        {
            this.service = service;
        }

        public async Task<FillViewModel> GetJournalFillViewModelAsync(int journalId)
        {
            Journal journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   a => a.WorkDays,
                                                                   b => b.Mentor);
            if (journal == null)
            {
                return null;
            }
            FillViewModel viewModel = new FillViewModel
            {
                Journal = journal,
                WorkDayModel = new WorkDay()
            };

            return viewModel;
        }

        public async Task CreateWorkDayAsync(ViewModels.WorkDays.CreateViewModel viewModel)
        {
            Journal journal = await service.GetByIdAsync(viewModel.JournalId);
            WorkDay newWorkDay = new WorkDay
            {
                JournalId = viewModel.JournalId,
                Day = viewModel.Day
            };
            journal.WorkDays.Add(newWorkDay);
            await service.SaveChangesAsync();
        }

        public async Task<ViewModels.Journals.IndexViewModel> GetJournalsIndexViewModelAsync()
        {
            IEnumerable<Journal> journals = await service.GetAllAsync(includeProperties: j => j.Mentor);
            ViewModels.Journals.IndexViewModel viewModel = new ViewModels.Journals.IndexViewModel
            {
                Journals = journals,
                JournalModel = new Journal(),
                MentorModel = new Mentor()
            };
            return viewModel;
        }

        public async Task<ViewModels.Journals.DetailsViewModel> GetJournalDetailsViewModelAsync(int journalId)
        {
            Journal journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   j => j.Mentor);
               
            if (journal == null)
            {
                return null;
            }
            ViewModels.Journals.DetailsViewModel viewModel = new ViewModels.Journals.DetailsViewModel
            {
                Journal = journal
            };
            return viewModel;
        }

        public async Task<int> CreateJournalAsync(ViewModels.Journals.CreateViewModel viewModel)
        {
            Journal newJournal = new Journal
            {
                Year = viewModel.Year,
                MentorId = viewModel.MentorId,
            };

            service.Create(newJournal);
            await service.SaveChangesAsync();
            return newJournal.Id;
        }

        public async Task<ViewModels.Journals.EditViewModel> GetEditJournalViewModelAsync(int journalId)
        {
            Journal journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   j => j.Mentor);
            if (journal == null)
            {
                return null;
            }

            ViewModels.Journals.EditViewModel viewModel = new ViewModels.Journals.EditViewModel
            {
                Year = journal.Year,
                Id = journal.Id
            };
            return viewModel;
        }

        public async Task UpdateJournalAsync(ViewModels.Journals.EditViewModel viewModel)
        {
            Journal updatedJournal = new Journal
            {
                Id = viewModel.Id,
                Year = viewModel.Year,
            };
            service.Update(updatedJournal, j => j.Year);
            await service.SaveChangesAsync();
        }

        public async Task<ViewModels.Journals.DeleteViewModel> GetDeleteJournalViewModelAsync(int journalId)
        {
            Journal journal = await service.GetByIdAsync(journalId);
            if (journal == null)
            {
                return null;
            }
            ViewModels.Journals.DeleteViewModel viewModel = new ViewModels.Journals.DeleteViewModel
            {
                Journal = journal
            };
            return viewModel;
        }

        public async Task DeleteJournalAsync(int journalId)
        {
            Journal journal = await service.GetByIdAsync(journalId);
            service.Delete(journal);
            await service.SaveChangesAsync();
        }

        public void Dispose()
        {
            IDisposable dispose = service as IDisposable;
            if(dispose != null)
            {
                dispose.Dispose();
            }
        }

        ViewModels.WorkDays.CreateViewModel IJournalsControllerService.GetCreateWorkDayViewModel(int journalId)
        {
            ViewModels.WorkDays.CreateViewModel viewModel = new ViewModels.WorkDays.CreateViewModel
            {
                Day = DateTime.Now,
                JournalId = journalId
            };
            return viewModel;
        }

        ViewModels.Journals.CreateViewModel IJournalsControllerService.GetCreateJournalViewModel(string mentorId)
        {
            Journal journal = new Journal
            {
                MentorId = mentorId,
                Year = DateTime.Now.Year
            };

            ViewModels.Journals.CreateViewModel viewModel = new ViewModels.Journals.CreateViewModel
            {
                MentorId = journal.MentorId,
                Year = journal.Year
            };
            return viewModel;
        }
    }
}