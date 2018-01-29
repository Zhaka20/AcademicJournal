using AcademicJournal.DAL.Models;
using System.Collections.Generic;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public abstract class AbstractMentorModel : AbstractApplicationUser
    {
        public virtual ICollection<AbstractStudentModel> Students { get; set; }
        public virtual ICollection<IAssignment> Assignments { get; set; }

        public virtual ICollection<IJournalModel> Journals { get; set; }
    }
}
