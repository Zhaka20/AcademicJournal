using AcademicJournal.AbstractModels.Interfaces;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface ISubmitFile : IFileInfoModel
    {
        int AssignmentId { get; set; }
        string StudentId { get; set; }
        ISubmissionModel Submission { get; set; }
    }
}
