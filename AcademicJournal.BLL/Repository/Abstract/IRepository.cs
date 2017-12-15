using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.Repository
{
    public interface IRepository<TItem, TKey> : IDisposable
    {
        IQueryable<TItem> List();
        TItem Get(TKey id);
        TItem Create(TItem user);
        bool Delete(TKey id);
        TItem Update(TItem user);
        bool SaveChanges();
    }
}
