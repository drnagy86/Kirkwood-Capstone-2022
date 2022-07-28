using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IActivityAccessor
    {
        int InsertActivity(Activity activity);
        List<Activity> SelectActivitiesByEventID(int eventID);
        List<ActivityVM> SelectActivitiesByEventIDForVM(int eventID);
        List<Activity> SelectActivitiesByEventIDAndEventDateID(int eventID, DateTime? eventDateID);
        List<Activity> SelectActivitiesBySublocationID(int sublocationID);
        List<ActivityVM> SelectActivitiesPastAndUpcomingDates();
        List<ActivityVM> SelectUserActivitiesPastAndUpcomingDates(int userID);
        List<Activity> SelectActivitiesBySupplierIDAndDate(int supplierID, DateTime date);
        int UpdateActivitySublocationByActivityID(int activityID, int? oldSublocationID, int? newSublocationID);
        List<Activity> SelectActivitiesBySupplierID(int supplierID);
        ActivityVM SelectActivityByActivityID(int activityID);
    }
}
