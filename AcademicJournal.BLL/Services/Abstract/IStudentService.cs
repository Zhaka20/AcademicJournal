using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    interface IStudentService<Tkey>
    {
        IEnumerable<Student> GetAllStudents();
        Student GetStudentByEmail(string studentEmail);
        Student GetStudentByID(Tkey id);
        Student UpdateStudent(Student student);
        Student CreateStudent(Student student);
        bool DeleteStudent(Tkey id);
        bool ChangeStudentPassword(string studentEmail, string newPassword);
    }
}
