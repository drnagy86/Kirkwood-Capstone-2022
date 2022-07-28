using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IActivityManager
    {
        int CreateActivity(Activity activity);
        List<Activity> RetrieveActivitiesByEventID(int eventID);
        List<ActivityVM> RetrieveActivitiesByEventIDForVM(int eventID);
        List<ActivityVM> RetrieveActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID);
        List<Activity> RetrieveActivitiesBySublocationID(int sublocationID);
        List<ActivityVM> RetreiveActivitiesPastAndUpcomingDates();
        List<ActivityVM> RetreiveUserActivitiesPastAndUpcomingDates(int userID);
        List<ActivityVM> RetrieveActivitiesBySupplierIDAndDate(int supplierID, DateTime date);
        bool UpdateActivitySublocationByActivityID(int activityID, int? oldSublocationID, int? newSublocationID);
        List<Activity> RetrieveActivitiesBySupplierID(int supplierID);
        ActivityVM RetrieveActivityVMByActivityID(int activityID);
    }
}
