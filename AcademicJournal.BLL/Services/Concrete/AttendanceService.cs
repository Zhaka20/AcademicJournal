using AcademicJournal.BLL.Services.Concrete.Common;
using AcademicJournal.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.BLL.Services.Abstract;

namespace AcademicJournal.BLL.Services.Concrete
{
    public class AttendanceService : GenericService<Attendance, int>, IAttendanceService
    {
        public AttendanceService(IGenericRepository<Attendance, int> repository) : base(repository)
        {
        }
    }
}
