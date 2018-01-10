using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class SubmitFile : FileInfo
    {
        public virtual Submission Submission { get; set; }
    }
}
