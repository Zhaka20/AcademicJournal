using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicJournal.BLL.MapperInterface
{
    public interface IObjectToObjectMapper
    {
        TReturn Map<TReturn, TInput>(TInput obj);
    }
}
