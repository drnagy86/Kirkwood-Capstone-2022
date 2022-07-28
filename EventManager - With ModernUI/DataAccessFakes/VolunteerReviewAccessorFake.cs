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
    /// The VolunteerReviewAccessorFake class for all fake volunteer review methods
    /// </summary>
    public class VolunteerReviewAccessorFake : IVolunteerReviewAccessor
    {
        private List<Reviews> _fakeVolunteerReviews = new List<Reviews>();
        private VolunteerAccessorFake _fakeVolunteerAccessor = new VolunteerAccessorFake();
        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Constructor which fills the _fakeVolunteerReviews with fake reviews
        /// </summary>
        public VolunteerReviewAccessorFake()
        {
            _fakeVolunteerReviews.Add(new Reviews()
            {
                ForeignID = 999999,
                ReviewID = 100000,
                FullName = "Tess Data",
                ReviewType = "Volunteer Review",
                Rating = 4,
                Review = "Did a really swell job. Thank you.",
                DateCreated = DateTime.Now,
                Active = true
            });

            _fakeVolunteerReviews.Add(new Reviews()
            {
                ForeignID = 999999,
                ReviewID = 100001,
                FullName = "Rob Data",
                ReviewType = "Volunteer Review",
                Rating = 2,
                Review = "Horrible job. The second worst possible.",
                DateCreated = DateTime.Now,
                Active = true
            });
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Method which selects all volunteer reviews by the passed in volunteerID
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>List of Reviews objects</returns>
        public List<Reviews> SelectVolunteerReviewsByVolunteerID(int volunteerID)
        {
            List<Reviews> volunteerReviews = new List<Reviews>();

            try
            {
                foreach(Reviews review in _fakeVolunteerReviews)
                {
                    if(review.ForeignID == volunteerID)
                    {
                        volunteerReviews.Add(review);
                    }
                }
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
        /// Inserts a review into the list of fake reviews
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>rowsAffected</returns>
        public int InsertVolunteerReview(Reviews review)
        {
            int rowsAffected = 0;
            List<Volunteer> fakeVolunteers = _fakeVolunteerAccessor.SelectAllVolunteers();
            foreach (var volunteer in fakeVolunteers)
            {
                if (volunteer.VolunteerID == review.ForeignID)
                {
                    _fakeVolunteerReviews.Add(review);
                    rowsAffected = 1;
                    break;
                }
            }
            if (rowsAffected == 0)
            {
                throw new ApplicationException("Invalid Volunteer ID");
            }
            return rowsAffected;
        }
    }
}
