using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IVolunteerReviewAccessor
    {
        List<Reviews> SelectVolunteerReviewsByVolunteerID(int volunteerID);
        int InsertVolunteerReview(Reviews review);
    }
}
