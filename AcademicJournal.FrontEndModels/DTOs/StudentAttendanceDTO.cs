using System;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class StudentAttendanceDTO
    {
        public int Id { get; set; }
        public StudentDTO Student { get; set; }
        public DateTime? Came { get; set; }
        public DateTime? Left { get; set; }
    }
}
