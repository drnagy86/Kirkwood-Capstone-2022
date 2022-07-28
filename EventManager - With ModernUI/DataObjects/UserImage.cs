using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/07
    /// 
    /// Description:
    /// The user image data object
    /// </summary>
    public class UserImage
    {
        public int ImageID { get; set; }
        public int UserID { get; set; }
        public string ImageName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
