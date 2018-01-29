using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.AbstractModels.Interfaces
{
    public abstract class AbstractFileInfoModel<TKey> : IFileInfo
    {
        public TKey Id { get; set; }
        public string FileGuid { get; set; }
        public string FileName { get; set; }     
    }
}

