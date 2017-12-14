﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicJournal.ViewModels
{
    public class CreateStudentVM
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string UserName
        {
            get { return Email;  }
        }
    }

    public class EditStudentVM
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string UserName
        {
            get { return Email; }
        }
    }

    public class DeleteStudentVM
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class ShowStudentVM
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Id { get; set; }
    }

}