using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.DAL.Models
{
    public class FileInfo
    {
        public int Id { get; set; }
        public string FileGuid { get; set; }
        public string FileName { get; set; }
    }
}
