using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels.Students;
using System.Collections.Generic;
using System.Linq;

namespace AcademicJournal.Extensions
{
    public static class StudentModelAndVM
    {
        public static ShowStudentViewModel ToShowStudentVM(this Student model)
        {
            ShowStudentViewModel vm  = new ShowStudentViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static Student ToStudentModel(this CreateStudentViewModel vm)
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
        public static StudentDetailsViewModel ToStudentDetailsVM(this Student model)
        {
            StudentDetailsViewModel vm = new StudentDetailsViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id
            };
            return vm;
        }

        public static EditStudentViewModel ToEditStudentVM(this Student model)
        {
            EditStudentViewModel vm = new EditStudentViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static IEnumerable<ShowStudentViewModel> ToShowStudentVMList(this IEnumerable<Student> students)
        {
            IEnumerable<ShowStudentViewModel> vmList = from student in students
                        select new ShowStudentViewModel()
                        {
                            Email = student.Email,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Id = student.Id,
                            PhoneNumber = student.PhoneNumber
                        };
            return  vmList;
        }

        public static Student ToStudentModel(this EditStudentViewModel vm)
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

        public static DeleteStudentViewModel ToDeleteStudentVM(this Student model)
        {
            DeleteStudentViewModel vm = new DeleteStudentViewModel
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