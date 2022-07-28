using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MVCPresentation.Models
{
    public class ReviewViewModel
    {
        public int ForeignID { get; set; }
        public string ReviewType { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Please choose a rating between 1 and 5")]
        public int Rating { get; set; }
        [StringLength(300, ErrorMessage = "Please keep your review to 300 characters or less.")]
        public string Review { get; set; }
        public bool Active { get; set; }
    }
}