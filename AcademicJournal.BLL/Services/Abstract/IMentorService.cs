using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    public interface IMentorService : IDisposable
    {
        Task<IEnumerable<Mentor>> GetAllMentorsAsync();
        Task<Mentor> GetMentorByEmailAsync(string mentorEmail);
        Task<Mentor> GetMentorByIDAsync(string id);
        void UpdateMentor(Mentor mentor);
        void CreateMentor(Mentor mentor);
        Task DeleteMentorAsync(string id);
        Task SaveChangesAsync();
    }
}
