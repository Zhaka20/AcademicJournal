﻿using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DAL.Context;

namespace AcademicJournal.DAL.Repositories
{
    public class WorkDayRepository : GenericRepository<WorkDayDTO, int>, IWorkDayRepository, IDisposable
    {
        public WorkDayRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
