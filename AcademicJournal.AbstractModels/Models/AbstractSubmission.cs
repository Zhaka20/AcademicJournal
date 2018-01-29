using AcademicJournal.AbstractModels.Interfaces;
using System;
using System.Collections.Generic;
namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface ISubmissionModel
    {
        byte? Grade { get; set; }
        bool Completed { get; set; }
        DateTime? DueDate { get; set; }
        DateTime? Submitted { get; set; }


        ISubmitFileModel SubmitFile { get; set; }

        string StudentId { get; set; }      
        AbstractStudentModel Student { get; set; }

        int AssignmentId { get; set; }  
        IAssignment Assignment { get; set; }

        ICollection<ICommentModel> Comments { get; set; }
    }
}
