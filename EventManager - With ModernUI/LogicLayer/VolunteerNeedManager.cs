using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Vinayak Deshpande
/// Created: 2022/02/24
/// 
/// manager for volunteer needs
/// </summary>

namespace LogicLayer
{
    public class VolunteerNeedManager : IVolunteerNeedManager
    {
        private IVolunteerNeedAccessor _volunteerNeedAccessor;
        public VolunteerNeedManager()
        {
            _volunteerNeedAccessor = new VolunteerNeedAccessor();
        }
        public VolunteerNeedManager(IVolunteerNeedAccessor needAccessor)
        {
            _volunteerNeedAccessor = needAccessor;
        }
        public bool AddVolunteerNeed(VolunteerNeed volunteerNeed)
        {
            int needAdded = _volunteerNeedAccessor.InsertVolunteerNeed(volunteerNeed.TaskID, volunteerNeed.NumTotalVolunteers);
            if (needAdded == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteVolunteerNeed(VolunteerNeed volunteerNeed)
        {
            int needDeleted = _volunteerNeedAccessor.DeleteVolunteerNeed(volunteerNeed.TaskID);
            if (needDeleted == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public VolunteerNeed RetrieveVolunteerNeedByTaskID(int taskID)
        {
            VolunteerNeed _need = _volunteerNeedAccessor.SelectVolunteerNeedByTaskID(taskID);
            return _need;
        }

        public bool UpdateCurrVolunteers(VolunteerNeed volunteerNeed, int newCurrVolunteers)
        {
            int currVoluntUpdated;
            if (newCurrVolunteers > volunteerNeed.NumCurrVolunteers)
            {
                currVoluntUpdated = _volunteerNeedAccessor.UpdateAddCurrVolunteers(volunteerNeed.TaskID);
            }
            else if (newCurrVolunteers < volunteerNeed.NumCurrVolunteers)
            {
                currVoluntUpdated = _volunteerNeedAccessor.UpdateSubtractCurrVolunteers(volunteerNeed.TaskID);
            }
            else
            {
                currVoluntUpdated = 0;
            }
            if (currVoluntUpdated == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateVolunteerNeed(VolunteerNeed volunteerNeed, int newTotalVolunteers)
        {
            int needUpdated = _volunteerNeedAccessor.UpdateVolunteerNeed(volunteerNeed.TaskID, newTotalVolunteers);

            if (needUpdated == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
