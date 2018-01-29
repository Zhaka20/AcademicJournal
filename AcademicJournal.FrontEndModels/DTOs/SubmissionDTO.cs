using System;
using System.Collections.Generic;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class SubmissionDTO
    {
        public byte? Grade { get; set; }
        public bool Completed { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Submitted { get; set; }

        public virtual SubmitFileDTO SubmitFile { get; set; }
        public string StudentId { get; set; }      
        public StudentDTO Student { get; set; }

        public int AssignmentId { get; set; }  
        public AssignmentDTO Assignment { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
    }
}
