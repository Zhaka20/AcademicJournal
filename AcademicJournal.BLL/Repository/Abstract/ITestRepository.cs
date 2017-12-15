﻿using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Repository
{
    public interface ITestRepository<TItem, TKey> : IRepository<TItem, TKey>
    {
    }
}
