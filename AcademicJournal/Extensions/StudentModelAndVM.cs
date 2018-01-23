using AcademicJournal.DataModel.Models;
using AcademicJournal.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace AcademicJournal.Extensions
{
    public static class StudentModelAndVM
    {
        public static ShowStudentVM ToShowStudentVM(this Student model)
        {
            ShowStudentVM vm  = new ShowStudentVM()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static Student ToStudentModel(this CreateStudentVM vm)
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
        public static StudentDetailsVM ToStudentDetailsVM(this Student model)
        {
            StudentDetailsVM vm = new StudentDetailsVM()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Id = model.Id
            };
            return vm;
        }

        public static EditStudentVM ToEditStudentVM(this Student model)
        {
            EditStudentVM vm = new EditStudentVM()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber
            };
            return vm;
        }

        public static IEnumerable<ShowStudentVM> ToShowStudentVMList(this IEnumerable<Student> students)
        {
            IEnumerable<ShowStudentVM> vmList = from student in students
                        select new ShowStudentVM()
                        {
                            Email = student.Email,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Id = student.Id,
                            PhoneNumber = student.PhoneNumber
                        };
            return  vmList;
        }

        public static Student ToStudentModel(this EditStudentVM vm)
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

        public static DeleteStudentVM ToDeleteStudentVM(this Student model)
        {
            DeleteStudentVM vm = new DeleteStudentVM
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