using AcademicJournal.AbstractBLL.AbstractServices.Common;
using AcademicJournal.FrontToBLL_DTOs.DTOs;
using System.Threading.Tasks;

namespace AcademicJournal.AbstractBLL.AbstractServices
{
    public interface IStudentService : IBasicCRUDService<StudentDTO,string>
    {
        Task<StudentDTO> GetStudentByEmailAsync(string studentEmail);
    }
}
