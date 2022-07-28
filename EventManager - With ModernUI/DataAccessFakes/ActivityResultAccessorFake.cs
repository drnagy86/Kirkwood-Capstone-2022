using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class ActivityResultAccessorFake : IActivityResultAccessor
    {
        private List<ActivityResult> _fakeActivityResults = new List<ActivityResult>();

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Constructor that adds fake Activity Results to the _fakeActivityResults list for test data
        /// 
        /// </summary>
        public ActivityResultAccessorFake()
        {
            _fakeActivityResults.Add(new ActivityResult() 
            { 
                ActivityResultRank = 1,
                ActivityID = 1000000,
                ActivityResultName = "Howie Harrison"
            });

            _fakeActivityResults.Add(new ActivityResult()
            {
                ActivityResultRank = 2,
                ActivityID = 1000000,
                ActivityResultName = "Isacc Jones"
            });

            _fakeActivityResults.Add(new ActivityResult()
            {
                ActivityResultRank = 1,
                ActivityID = 1000001,
                ActivityResultName = "Paula"
            });

            _fakeActivityResults.Add(new ActivityResult()
            {
                ActivityResultRank = 1,
                ActivityID = 1000002,
                ActivityResultName = "Team \"Tic-Tac-Toad\""
            });
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/01
        /// 
        /// Description:
        /// Selects fake Activity Results that belong to a specific activity for testing
        /// 
        /// </summary>
        /// <param name="activityID"></param>
        /// <returns>A List of ActivityResults for an Activity</returns>

        public List<ActivityResult> SelectActivityResultsByActivityID(int activityID)
        {
            List<ActivityResult> results = new List<ActivityResult>();

            foreach(ActivityResult activityResult in _fakeActivityResults)
            {
                if(activityResult.ActivityID == activityID)
                {
                    results.Add(activityResult);
                }
            }

            return results;
        }
    }
}
