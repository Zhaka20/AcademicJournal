using AcademicJournal.AbstractBLL.AbstractServices.Common;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using System.Threading.Tasks;

namespace AcademicJournal.AbstractBLL.AbstractServices
{
    public interface IMentorService : IBasicCRUDService<MentorDTO, string>
    {
        Task<MentorDTO> GetMentorByEmailAsync(string mentorEmail);
        Task AcceptStudentAsync(string studentId, string mentorId);
        Task RemoveStudentAsync(string studentId, string mentorId);
    }
}
