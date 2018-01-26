using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class AssignmentFileViewModel : FileInfoViewModel
    {
        public IEnumerable<AssignmentViewModel> Assignments { get; set; }
    }
}
