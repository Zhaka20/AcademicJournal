using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.Services.Abstractions
{
    public interface IFileStreamWithName
    {

        byte[] FileStream { get; set; }
        string FileName { get; set; }
    }
}
