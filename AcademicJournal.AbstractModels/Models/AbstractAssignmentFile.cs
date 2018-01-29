using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface IAssignmentFileModel : IFileInfoModel
    {
        ICollection<IAssignmentModel<int,int,string>> Assignments { get; set; }
    }
}
