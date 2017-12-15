using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    interface IMentorService<Tkey>
    {
        IEnumerable<Mentor> GetAllMentors();
        Mentor GetMentorByEmail(string mentorEmail);
        Mentor GetMentorByID(Tkey id);
        Mentor UpdateMentor(Mentor mentor);
        Mentor CreateMentor(Mentor mentor);
        bool DeleteMentor(Tkey id);
        bool ChangeMentorPassword(string mentorEmail, string newPassword);
    }
}
