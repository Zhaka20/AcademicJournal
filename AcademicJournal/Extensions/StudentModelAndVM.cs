using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Students;
using System.Collections.Generic;
using System.Linq;

namespace AcademicJournal.Extensions
{
    public static class StudentModelAndVM
    {
        public static ShowViewModel ToShowStudentVM(this Student model)
        {
            ShowViewModel vm  = new ShowViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static Student ToStudentModel(this CreateViewModel vm)
        {
            Student model = new Student
            {
                UserName = vm.UserName,
                Email = vm.Email,
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                PhoneNumber = vm.PhoneNumber
            };
            return model;
        }
        public static DetailsViewModel ToStudentDetailsVM(this Student model)
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

        public static EditViewModel ToEditStudentVM(this Student model)
        {
            EditViewModel vm = new EditViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static IEnumerable<ShowViewModel> ToShowStudentVMList(this IEnumerable<Student> students)
        {
            IEnumerable<ShowViewModel> vmList = from student in students
                        select new ShowViewModel()
                        {
                            Email = student.Email,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Id = student.Id,
                            PhoneNumber = student.PhoneNumber
                        };
            return  vmList;
        }

        public static Student ToStudentModel(this EditViewModel vm)
        {        
                Student student = new Student
                {
                    UserName = vm.UserName,
                    Email = vm.Email,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    PhoneNumber = vm.PhoneNumber,
                    Id = vm.Id
                };
            return student;
        }

        public static DeleteViewModel ToDeleteStudentVM(this Student model)
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