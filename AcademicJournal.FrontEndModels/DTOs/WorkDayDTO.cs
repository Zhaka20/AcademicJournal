using System;
using System.Collections.Generic;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class WorkDayDTO
    {
        public WorkDayDTO()
        {
            Attendances = new List<AttendanceDTO>();
        }
        public int Id { get; set; }
        public DateTime Day { get; set; }
        public int JournalId { get; set; }
        public JournalDTO Journal { get; set; }
        public ICollection<AttendanceDTO> Attendances { get; set; }
    }
}
