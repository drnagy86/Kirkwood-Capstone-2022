using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataObjects;

namespace MVCPresentation.Models
{
    public class SupplierDetailsViewModel
    {
        public Supplier Supplier { get; set; }
        public List<string> SupplierImages { get; set; }
        public List<string> SupplierTags { get; set; }
        public List<Reviews> SupplierReviews { get; set; }
        public bool CanEdit { get; set; }
    }

    public class SupplierServicesViewModel
    {
        public Supplier Supplier { get; set; }
        public List<ServiceVM> Services { get; set; }
        public bool CanEdit { get; set; }
    }

    public class EditSupplierModel : Supplier
    {
        [Required(ErrorMessage = "You must enter a name.")]
        [StringLength(160, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "Name")]
        public string NewName { get; set; }

        [StringLength(3000, ErrorMessage = "The {0} must be less than {1} characters long.")]
        [Display(Name = "Description")]
        public string NewDescription { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression("\\+{0,1}[0-9]{0,1}[\\-]{0,1}\\({0,1}[0-9]{3}\\){0,1}[\\-]{0,1}[0-9]{3}[\\-]{0,1}[0-9]{4}", ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Phone Number")]
        public string NewPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email Address")]
        public string NewEmail { get; set; }

        [Display(Name = "Address Line 1")]
        [Required(ErrorMessage = "Please enter an address")]
        [StringLength(100, ErrorMessage = "Address can not be longer than 100 characters.")]
        public string NewAddress1 { get; set; }

        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "Address can not be longer than 100 characters.")]
        public string NewAddress2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter a city.")]
        [StringLength(100, ErrorMessage = "City can not be longer than 100 characters.")]
        public string NewCity { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Please enter a state.")]
        [StringLength(100, ErrorMessage = "State can not be longer than 100 characters.")]
        public string NewState { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Please enter a zip code")]
        [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)", ErrorMessage = "Not a valid zip code.")]
        public string NewZipCode { get; set; }
    }
}