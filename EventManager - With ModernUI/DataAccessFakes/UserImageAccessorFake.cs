using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/09
    /// 
    /// Description:
    /// Accessor Fake for User Image fakes
    /// </summary>
    public class UserImageAccessorFake : IUserImageAccessor
    {
        private List<UserImage> _fakeUserImages = new List<UserImage>();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Constructor for UserImageAccessorFake that adds the fake images to the 
        /// _userImages
        /// </summary>
        public UserImageAccessorFake()
        {
            _fakeUserImages.Add(new UserImage()
            {
                ImageID = 100000,
                UserID = 999999,
                ImageName = "f515b7f6-a7f5-4bd5-b489-75d78e6a185f.jpg",
                DateCreated = DateTime.Now
            });
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Method that goes through the list of fake user images and returns a list
        /// of images that match the passed through userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of UserImage objects</returns>
        public List<UserImage> SelectUserImagesByUserID(int userID)
        {
            List<UserImage> userImages = new List<UserImage>();

            try
            {
                foreach(UserImage image in _fakeUserImages)
                {
                    if(image.UserID == userID)
                    {
                        userImages.Add(image);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return userImages;
        }
    }
}
