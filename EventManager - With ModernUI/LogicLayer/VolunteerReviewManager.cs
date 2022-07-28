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
    /// Created: 2022/03/10
    /// 
    /// Description:
    /// The VolunteerReviewManager class for all logic layer methods relating to Volunteer Reviews
    /// </summary>
    public class VolunteerReviewManager : IVolunteerReviewManager
    {
        IVolunteerReviewAccessor _volunteerReviewAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Default constructor which sets the _volunteerReviewAccessor to the live accessor
        /// </summary>
        public VolunteerReviewManager()
        {
            _volunteerReviewAccessor = new VolunteerReviewAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Custom constructor which sets the _volunteerReviewAccessor to the fake accessor
        /// </summary>
        public VolunteerReviewManager(IVolunteerReviewAccessor volunteerReviewAccessor)
        {
            _volunteerReviewAccessor = volunteerReviewAccessor;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Method which retrieves all volunteer reviews by the passed in volunteerID
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>List of Reviews objects</returns>
        public List<Reviews> RetrieveVolunteerReviewsByVolunteerID(int volunteerID)
        {
            List<Reviews> volunteerReviews = new List<Reviews>();

            try
            {
                volunteerReviews = _volunteerReviewAccessor.SelectVolunteerReviewsByVolunteerID(volunteerID);
            }
            catch (Exception)
            {

                throw;
            }

            return volunteerReviews;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Creates a volunteer review in the database or throws an error if the review is invalid
        /// </summary>
        /// <param name="review"></param>
        /// <returns>rows affected</returns>
        public int CreateVolunteerReview(Reviews review)
        {
            try
            {
                if (review.Rating < 1 || review.Rating > 5)
                {
                    throw new ArgumentException("Rating must be between 1 and 5.");
                }
                else if (review.Review.Length > 3000)
                {
                    throw new ArgumentException("Please keep review under 3000 characters.");
                }
                return _volunteerReviewAccessor.InsertVolunteerReview(review);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
