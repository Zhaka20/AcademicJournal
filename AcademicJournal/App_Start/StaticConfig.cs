using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcademicJournal.App_Start
{
    public static class StaticConfig
    {
        public static PasswordValidator GetPasswordValidator()
        {
           var validator =  new PasswordValidator
            {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            return validator;
        }

        public const string DEFAULT_PASSWORD = "1";
    }
}