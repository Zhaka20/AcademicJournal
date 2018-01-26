using AcademicJournal.AbstractBLL.AbstractServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using AcademicJournal.DAL.Context;
using AcademicJournal.DataModel.Models;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class StudentService : GenericService<Student, string>, IStudentService
    {
        public StudentService(IStudentRepository repository) : base(repository)
        {
        }
              
        public async Task<Student> GetStudentByEmailAsync(string studentEmail)
        {
            ThrowIfNull(studentEmail);
            return await repository.GetFirstOrDefaultAsync(s => s.Email == studentEmail);
        }

        private void ThrowIfNull(object arg)
        {
            if (arg == null) throw new ArgumentNullException();
        }

    }
}
