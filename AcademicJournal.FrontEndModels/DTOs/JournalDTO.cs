using System.Collections.Generic;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class JournalDTO
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public ICollection<WorkDayDTO> WorkDays{ get; set; }

        public string MentorId { get; set; }
        public MentorDTO Mentor { get; set; }
    }
}
