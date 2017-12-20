using AcademicJournal.BLL.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DAL.Models;
using AcademicJournal.BLL.Repository.Abstract;
using System.Data.Entity;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class MentorService : IMentorService
    {
        IMentorRepository mentorRepository;
        public MentorService(IMentorRepository repo)
        {
            this.mentorRepository = repo;
        }

        public void CreateMentor(Mentor mentor)
        {
            mentorRepository.Add(mentor);
        }

        public void DeleteMentor(string id)
        {
            mentorRepository.Delete(id);
        }

        public async Task<IEnumerable<Mentor>> GetAllMentorsAsync()
        {
            return await mentorRepository.Query().ToListAsync();
        }

        public async Task<Mentor> GetMentorByEmailAsync(string mentorEmail)
        {
            return await mentorRepository.Query().Where(m => m.Email == mentorEmail).FirstOrDefaultAsync();
        }

        public async Task<Mentor> GetMentorByIDAsync(string id)
        {
            return await mentorRepository.Query().Where(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await mentorRepository.SaveChangesAsync();
        }

        public void UpdateMentor(Mentor mentor)
        {
            mentorRepository.Update(mentor);
        }
        public void Dispose()
        {
            mentorRepository.Dispose();
        }
    }
}
