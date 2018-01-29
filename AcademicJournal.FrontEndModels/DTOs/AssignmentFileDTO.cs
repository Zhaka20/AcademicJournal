using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class AssignmentFileDTO : FileInfoDTO
    {
        public ICollection<AssignmentDTO> Assignments { get; set; }
    }
}
