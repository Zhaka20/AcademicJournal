using AcademicJournal.AbstractBLL.AbstractServices;
using AcademicJournal.BLL.Services.Concrete;
using AcademicJournal.DAL.Context;
using AcademicJournal.DAL.Repositories;
using AcademicJournal.DAL.Repositories.Common;
using AcademicJournal.DALAbstraction.AbstractRepositories;
using AcademicJournal.DALAbstraction.AbstractRepositories.Common;
using AcademicJournal.DataModel.Models;
using AcademicJournal.Services.Abstractions;
using AcademicJournal.Services.ControllerServices;
using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace AcademicJournal.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
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

            //Repositories
            builder.RegisterGeneric(typeof(GenericRepository<,>)).As(typeof(GenericRepository<,>));
            builder.RegisterType<AssignmentFileRepository>().As<IAssignmentFileRepository>().InstancePerRequest();
            builder.RegisterType<AssignmentRepository>().As<IAssignmentRepository>().InstancePerRequest();
            builder.RegisterType<AttendanceRepository>().As<IAttendanceRepository>().InstancePerRequest();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerRequest();
            builder.RegisterType<JournalRepository>().As<IJournalRepository>().InstancePerRequest();
            builder.RegisterType<MentorRepository>().As<IMentorRepository>().InstancePerRequest();
            builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerRequest();
            builder.RegisterType<SubmissionRepository>().As<ISubmissionRepository>().InstancePerRequest();
            builder.RegisterType<SubmitFileRepository>().As<ISubmitFileRepository>().InstancePerRequest();
            builder.RegisterType<WorkDayRepository>().As<IWorkDayRepository>().InstancePerRequest();

            //Entity services
            builder.RegisterType<AssignmentFileService>().As<IAssignmentFileService>().InstancePerRequest();
            builder.RegisterType<AssignmentService>().As<IAssignmentService>().InstancePerRequest();
            builder.RegisterType<AttendanceService>().As<IAttendanceService>().InstancePerRequest();
            builder.RegisterType<JournalService>().As<IJournalService>().InstancePerRequest();
            builder.RegisterType<MentorService>().As<IMentorService>().InstancePerRequest();
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerRequest();
            builder.RegisterType<SubmissionService>().As<ISubmissionService>().InstancePerRequest();
            builder.RegisterType<SubmitFileService>().As<ISubmitFileService>().InstancePerRequest();
            builder.RegisterType<WorkDayService>().As<IWorkDayService>().InstancePerRequest();

            //Controller services
            builder.RegisterType<MentorsControllerService>().As<IMentorsControllerService>().InstancePerRequest();
            builder.RegisterType<StudentsControllerService>().As<IStudentsControllerService>().InstancePerRequest();
            builder.RegisterType<JournalsControllerService>().As<IJournalsControllerService>().InstancePerRequest();
            builder.RegisterType<SubmissionsControllerService>().As<ISubmissionsControllerService>().InstancePerRequest();
            builder.RegisterType<WorkDaysControllerService>().As<IWorkDaysControllerService>().InstancePerRequest();
            builder.RegisterType<AttendancesControllerService>().As<IAttendancesControllerService>().InstancePerRequest();
            builder.RegisterType<AssignmentsControllerService>().As<IAssignmentsControllerService>().InstancePerRequest();


            // Set the dependency resolver to be Autofac.
            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}