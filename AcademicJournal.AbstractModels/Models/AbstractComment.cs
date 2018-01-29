using AcademicJournal.DAL.Models;
using System;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public interface ICommentModel<TKey, TAssignmentKey>
    {
       TKey Id { get; set; }
       string Text { get; set; }

       DateTime? Created { get; set; }
       DateTime? Edited { get; set; }

       TAssignmentKey AssignmentId { get; set; }
       IAssignmentModel<int,int,string> Assignment { get; set; }

       string AuthorId { get; set; }
       ApplicationUser Author { get; set; }
    }
}
