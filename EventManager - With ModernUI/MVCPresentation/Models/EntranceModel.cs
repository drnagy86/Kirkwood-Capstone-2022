using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataObjects;

namespace MVCPresentation.Models
{
    public class EntranceModel
    {
        public int EntranceID { get; set; }
        public int LocationID { get; set; }

        [Required(ErrorMessage = "You must enter a name.")]
        [StringLength(100, ErrorMessage = "The {0} must be less than {1} characters.")]
        [Display(Name = "Name")]
        public string EntranceName { get; set; }

        [Required(ErrorMessage = "You must enter a description.")]
        [StringLength(255, ErrorMessage = "The {0} must be less than {1} characters.")]
        public string Description { get; set; }
    }

    public class EditEntranceModel : EntranceModel
    {
        public string OldEntranceName { get; set; }
        public string OldDescription { get; set; }
    }

    public class ViewEntrancesModel
    {
        public List<Entrance> Entrances { get; set; }
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
}