using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels;

namespace AcademicJournal.Extensions
{
    public static class MentorModelAndVM
    {
        public static MentorDetailsVM ToMentorDetailsVM(this Mentor model)
        {
            MentorDetailsVM vm = new MentorDetailsVM()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id
            };
            return vm;
        }

        public static Mentor ToMentorModel(this CreateMentorVM vm)
        {
            Mentor model = new Mentor()
            {
                Email = vm.Email,
                UserName = vm.UserName,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber
            };
            return model;
        }

        public static EditMentorVM ToEditMentorVM(this Mentor model)
        {
            EditMentorVM vm = new EditMentorVM
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static Mentor ToMentorModel(this EditMentorVM vm)
        {
            Mentor model = new Mentor
            {
                UserName = vm.UserName,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber,
                Id = vm.Id
            };
            return model;
        }

        public static DeleteMentorVM ToDeleteMentorVM(this Mentor model)
        {
            DeleteMentorVM vm = new DeleteMentorVM
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }
    }
}