﻿using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class JournalService : GenericService<Journal, int>
    {
        public JournalService(IGenericRepository<Journal, int> repository) : base(repository)
        {
        }
    }
}
