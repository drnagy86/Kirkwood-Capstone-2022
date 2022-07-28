using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Kris Howell
    /// Created: 2022/01/27
    /// 
    /// Description:
    /// Data object for a supplier
    /// 
    /// </summary>
    public class Supplier
    {
        public int SupplierID { get; set; }
        public int? UserID { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter a Name")]
        [StringLength(160, ErrorMessage = "The Supplier name can not be longer than 160 characters.")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [StringLength(3000, ErrorMessage = "The Description can not be longer than 3000 characters.")]
        public string Description { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        [RegularExpression("\\+{0,1}[0-9]{0,1}[\\-]{0,1}\\({0,1}[0-9]{3}\\){0,1}[\\-]{0,1}[0-9]{3}[\\-]{0,1}[0-9]{4}", ErrorMessage = "Invalid Phone Number")]
        [Display(Name = "Phone Number")]
        [Required]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        public string TypeID { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter an address")]
        [StringLength(100, ErrorMessage = "Address can not be longer than 100 characters.")]
        public string Address1 { get; set; }
        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "Address can not be longer than 100 characters.")]
        public string Address2 { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please enter a city.")]
        [StringLength(100, ErrorMessage = "City can not be longer than 100 characters.")]
        public string City { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "Please enter a state.")]
        [StringLength(100, ErrorMessage = "State can not be longer than 100 characters.")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Please enter a zip code")]
        [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)", ErrorMessage = "Not a valid zip code.")]
        public string ZipCode { get; set; }
        public int AverageRating { get; set; }
        public List<string> Tags { get; set; }
        public bool Active { get; set; }
        public bool? Approved { get; set; }
    }
    /// <summary>
    /// Derrick Nagy
    /// Created: 2022/04/05
    /// 
    /// Description:
    /// Create SupplierVM with their availablity for three months
    /// </summary>
    public class SupplierVM : Supplier
    {
        public List<DateTime> ThreeMonthAvailability { get; set; }

    }
}
