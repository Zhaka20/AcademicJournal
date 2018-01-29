using System.Collections.Generic;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface IJournalModel
    {
        int Id { get; set; }
        int Year { get; set; }
        ICollection<IWorkDay> WorkDays{ get; set; }

        string MentorId { get; set; }
        AbstractMentorModel Mentor { get; set; }
    }
}
