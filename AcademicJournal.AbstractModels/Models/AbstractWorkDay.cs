using System;
using System.Collections.Generic;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface IWorkDay<TKey>
    {
        TKey Id { get; set; }
        DateTime Day { get; set; }
        int JournalId { get; set; }
        IJournalModel Journal { get; set; }
        ICollection<IAttendanceModel> Attendances { get; set; }
    }
}
