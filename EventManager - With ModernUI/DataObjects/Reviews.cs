using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataObjects
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/04
    /// 
    /// Description:
    /// The locationreview data object
    /// 
    /// Christopher Repko
    /// Updated: 2022/02/10
    /// 
    /// Updated to make generic and match documentation, as all review types have the same information.
    /// </summary>
    public class Reviews
    {
        public int ForeignID { get; set; }
        public int ReviewID { get; set; }

        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ReviewType { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        [StringLength(3000, ErrorMessage = "Please keep review under 3000 characters long.")]
        public string Review { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }
    }
}
