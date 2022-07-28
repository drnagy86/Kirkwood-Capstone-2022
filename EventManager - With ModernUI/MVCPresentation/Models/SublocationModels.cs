using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataObjects;

namespace MVCPresentation.Models
{
    public class SublocationModel
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public List<Sublocation> Sublocations { get; set; }
        public bool CanEdit { get; set; }
    }

    public class EditSublocationModel : Sublocation
    {

        [Required(ErrorMessage = "You must enter a name.")]
        [StringLength(160, MinimumLength = 2, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "Name")]
        public string NewSublocationName { get; set; }

        [StringLength(3000, ErrorMessage = "The {0} must be less than {1} characters long.")]
        [Display(Name = "Description")]
        public string NewSublocationDescription { get; set; }
    }
}