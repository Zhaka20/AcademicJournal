using AcademicJournal.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Repository.Abstract
{
    public interface ITestRepository : IRepository<Test, int> 
    {
    }
}
