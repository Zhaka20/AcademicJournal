using System;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class AttendanceDTO
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public StudentDTO Student{ get; set; }

        public int WorkDayId { get; set; }
        public WorkDayDTO Day { get; set; }

        public DateTime? Come { get; set; }
        public DateTime? Left { get; set; }
    }
}
