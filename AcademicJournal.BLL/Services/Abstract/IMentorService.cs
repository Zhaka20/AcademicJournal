using AcademicJournal.BLL.Services.Abstract.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    public interface IMentorService : IGenericService<Mentor,string>
    {
        Task<Mentor> GetMentorByEmailAsync(string mentorEmail);
        Task AcceptStudentAsync(string studentId, string mentorId);
        Task RemoveStudentAsync(string studentId, string mentorId);
    }
}
