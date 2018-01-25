using AcademicJournal.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcademicJournal.DataModel.Models;
using AcademicJournal.BLL.Services.Abstract;
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

        public async Task<JournalFillVM> GetJournalFillViewModelAsync(int journalId)
        {
            Journal journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   a => a.WorkDays,
                                                                   b => b.Mentor);
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

        public WorkDayCreateViewModel GetCreateWorkDayViewModel(int journalId)
        {
            WorkDayCreateViewModel viewModel = new WorkDayCreateViewModel
            {
                Day = DateTime.Now,
                JournalId = journalId
            };
            return viewModel;
        }

        public async Task CreateWorkDayAsync(WorkDayCreateViewModel viewModel)
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

        public async Task<JournalIndexViewModel> GetJournalsIndexViewModelAsync()
        {
            IEnumerable<Journal> journals = await service.GetAllAsync(includeProperties: j => j.Mentor);
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
            Journal journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   j => j.Mentor);
               
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

            service.Create(newJournal);
            await service.SaveChangesAsync();
            return newJournal.Id;
        }

        public async Task<EditJournalVM> GetEditJournalViewModelAsync(int journalId)
        {
            Journal journal = await service.GetFirstOrDefaultAsync(j => j.Id == journalId,
                                                                   j => j.Mentor);
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
            service.Update(updatedJournal, j => j.Year);
            await service.SaveChangesAsync();
        }

        public async Task<DeleteJournalVM> GetDeleteJournalViewModelAsync(int journalId)
        {
            Journal journal = await service.GetByIdAsync(journalId);
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

    }
}