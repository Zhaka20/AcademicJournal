﻿using AcademicJournal.BLL.Services.Abstract.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Services.Abstract
{
    public interface IStudentService : IGenericService<Student,string>
    {
        Task<Student> GetStudentByEmailAsync(string studentEmail);
    }
}
