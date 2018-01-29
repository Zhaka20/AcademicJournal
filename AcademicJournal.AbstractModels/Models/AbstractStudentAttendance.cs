using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface IStudentAttendanceModel
    {
        int Id { get; set; }
        AbstractStudentModel Student { get; set; }
        DateTime? Came { get; set; }
        DateTime? Left { get; set; }
    }
}
