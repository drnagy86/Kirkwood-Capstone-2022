using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using LogicLayerInterfaces;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;

namespace LogicLayer
{
    public class VolunteerApplicationsManager : IVolunteerApplicationsManager
    {
        IVolunteerApplicationsAccessor _volunteersApplicationsAccessor = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Constructor that uses default accessor
        /// </summary>
        public VolunteerApplicationsManager()
        {

            _volunteersApplicationsAccessor = new VolunteerApplicationsAccessor();
        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Constructor that uses the accessor passed in 
        /// </summary>
        /// <param name="volunteerApplicationsAccessor"></param>
        public VolunteerApplicationsManager(IVolunteerApplicationsAccessor volunteerApplicationsAccessor)
        {
            _volunteersApplicationsAccessor = volunteerApplicationsAccessor;
        }

        public bool CreateVolunteerApplication(int userID, Availability availability)
        {

            int numberOfTablesUpdatedInDatabase = 4;

            bool result = false;
            bool noDaysOfWeekSelected = (
                !availability.Sunday &&
                !availability.Monday && 
                !availability.Tuesday &&
                !availability.Wednesday &&
                !availability.Thursday &&
                !availability.Friday &&
                !availability.Saturday);
            //result = true;

            if (noDaysOfWeekSelected)
            {
                throw new ApplicationException("No days of the week selected");
            }

            if (availability.TimeStart == null)
            {
                throw new ApplicationException("No start time selected");
            }

            if (availability.TimeEnd == null)
            {
                throw new ApplicationException("No end time selected");
            }

            if (availability.TimeStart > availability.TimeEnd)
            {
                throw new ApplicationException("Start time before end time");
            }

            try
            {
                result = (numberOfTablesUpdatedInDatabase == _volunteersApplicationsAccessor.InsertVolunteerApplication(userID, availability));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!result)
            {
                throw new ApplicationException("No user found");
            }

            return result;
        }
    }
}
