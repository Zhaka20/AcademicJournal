using System;
using System.Collections.Generic;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface IAssignmentModel<TKey, TFileKey,TCreatorKey>
    {
        TKey AssignmentId { get; set; }
        string Title { get; set; }

        TFileKey AssignmentFileId { get; set; }
        IAssignmentFileModel AssignmentFile { get; set; }

        TCreatorKey CreatorId { get; set; }
        AbstractMentorModel Creator { get; set; }

        DateTime Created { get; set; }
        ICollection<ISubmissionModel> Submissions { get; set; }
    }
}
