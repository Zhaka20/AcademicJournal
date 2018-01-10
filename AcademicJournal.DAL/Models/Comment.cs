using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Edited { get; set; }

        [ForeignKey("Assignment")]
        public int AssignmentId { get; set; }
        public virtual Assignment Assignment { get; set; }

        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual ApplicationUser Author { get; set; }
    }
}
