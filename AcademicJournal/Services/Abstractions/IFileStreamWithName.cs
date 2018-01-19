using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IFileStreamWithInfo
    {

        byte[] FileStream { get; set; }
        string FileName { get; set; }
        string FileType { get; set; }
    }
}
