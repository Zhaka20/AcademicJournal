using System;
using System.Collections.Generic;

namespace AcademicJournal.FrontToBLL_DTOs.DTOs
{
    public class AssignmentDTO
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }

        public int? AssignmentFileId { get; set; }
        public AssignmentFileDTO AssignmentFile { get; set; }

        public string CreatorId { get; set; }
        public MentorDTO Creator { get; set; }

        public DateTime Created { get; set; }
        public ICollection<SubmissionDTO> Submissions { get; set; }
    }
}
