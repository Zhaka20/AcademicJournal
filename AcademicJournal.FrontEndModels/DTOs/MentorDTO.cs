using System.Collections.Generic;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class MentorDTO : ApplicationUserDTO
    {
        public ICollection<StudentDTO> Students { get; set; }
        public ICollection<AssignmentDTO> Assignments { get; set; }
               
        public ICollection<JournalDTO> Journals { get; set; }
    }
}
