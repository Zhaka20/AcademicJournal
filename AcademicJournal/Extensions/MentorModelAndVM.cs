using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Mentors;

namespace AcademicJournal.Extensions
{
    public static class MentorModelAndVM
    {
        public static MentorDetailsViewModel ToMentorDetailsVM(this Mentor model)
        {
            MentorDetailsViewModel vm = new MentorDetailsViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id
            };
            return vm;
        }

        public static Mentor ToMentorModel(this CreateMentorViewModel vm)
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

        public static EditMentorViewModel ToEditMentorVM(this Mentor model)
        {
            EditMentorViewModel vm = new EditMentorViewModel
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static Mentor ToMentorModel(this EditMentorViewModel vm)
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

        public static DeleteMentorViewModel ToDeleteMentorVM(this Mentor model)
        {
            DeleteMentorViewModel vm = new DeleteMentorViewModel
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