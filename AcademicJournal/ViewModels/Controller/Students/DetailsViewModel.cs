using System.ComponentModel.DataAnnotations;

namespace AcademicJournal.ViewModels.Controller.Students
{
    public class DetailsViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string Id { get; set; }
    }
}