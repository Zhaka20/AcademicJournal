using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.ViewModels.Shared.EntityViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Edited { get; set; }

        public int AssignmentId { get; set; }
        public virtual AssignmentViewModel Assignment { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUserViewModel Author { get; set; }
    }
}
