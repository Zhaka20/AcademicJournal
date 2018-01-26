using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Controller.Mentors;

namespace AcademicJournal.Extensions
{
    public static class MentorModelAndVM
    {
        public static DetailsViewModel ToMentorDetailsVM(this Mentor model)
        {
            DetailsViewModel vm = new DetailsViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id
            };
            return vm;
        }

        public static Mentor ToMentorModel(this CreateViewModel vm)
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

        public static EditViewModel ToEditMentorVM(this Mentor model)
        {
            EditViewModel vm = new EditViewModel
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static Mentor ToMentorModel(this EditViewModel vm)
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

        public static DeleteViewModel ToDeleteMentorVM(this Mentor model)
        {
            DeleteViewModel vm = new DeleteViewModel
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