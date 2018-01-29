using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.Services.Common
{
    public interface IObjectMapper
    {
        TReturn Map<TInput,TReturn>(TInput objectToMap);
    }
}