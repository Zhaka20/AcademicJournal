﻿using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    public interface IStudentService : IDisposable
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByEmailAsync(string studentEmail);
        Task<Student> GetStudentByIDAsync(string id);
        void UpdateStudent(Student student);
        void CreateStudent(Student student);
        Task DeleteStudentAsync(string id);
        Task SaveChangesAsync();
    }
}
