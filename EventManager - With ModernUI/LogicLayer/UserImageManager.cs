using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/09
    /// 
    /// Description:
    /// Manager class for UserImageManager methods
    /// </summary>
    public class UserImageManager : IUserImageManager
    {
        IUserImageAccessor _userImageAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Default constructor that uses the real accessor
        /// </summary>
        public UserImageManager()
        {
            _userImageAccessor = new UserImageAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Custom constructor that uses the fake accessor
        /// </summary>
        public UserImageManager(IUserImageAccessor userImageAccessor)
        {
            _userImageAccessor = userImageAccessor;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Method to retrieve user images by userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of UserImage objects</returns>
        public List<UserImage> RetrieveUserImagesByUserID(int userID)
        {
            List<UserImage> userImages = new List<UserImage>();

            try
            {
                userImages = _userImageAccessor.SelectUserImagesByUserID(userID);
            }
            catch (Exception)
            {

                throw;
            }

            return userImages;
        }
    }
}
