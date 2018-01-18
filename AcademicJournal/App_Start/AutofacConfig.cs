using AcademicJournal.BLL.Services.Abstract;
using AcademicJournal.BLL.Services.Concrete;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Models;
using AcademicJournal.Services.Abstractions;
using AcademicJournal.Services.ControllerServices;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            builder.RegisterType<ApplicationDbContext>().AsSelf().Instance‌​PerRequest();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationDbContext>())).AsImplementedInterfaces().InstancePerRequest();
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();
            builder.Register(c => new IdentityFactoryOptions<ApplicationUserManager>
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Application​")
            });

            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerRequest();
            builder.RegisterType<MentorService>().As<IMentorService>().InstancePerRequest();
            
            //Controller services
            builder.RegisterType<MentorsControllerService>().As<IMentorsControllerService>().InstancePerRequest();
            builder.RegisterType<StudentsControllerService>().As<IStudentsControllerService>().InstancePerRequest();
            builder.RegisterType<JournalsControllerService>().As<IJournalsControllerService>().InstancePerRequest();
            builder.RegisterType<SubmissionsControllerService>().As<ISubmissionsControllerService>().InstancePerRequest();
            builder.RegisterType<WorkDaysControllerService>().As<IWorkDaysControllerService>().InstancePerRequest();
            builder.RegisterType<AttendancesControllerService>().As<IAttendancesControllerService>().InstancePerRequest();
            builder.RegisterType<AssignmentsControllerService>().As<IAssignmentsControllerService>().InstancePerRequest();


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}