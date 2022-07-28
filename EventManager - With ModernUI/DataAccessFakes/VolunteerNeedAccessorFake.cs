using DataAccessInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Vinayak Deshpande
/// Created: 2022/03/15
/// 
/// Description: fake accessor for the volunteer needs
/// </summary>
namespace DataAccessFakes
{
    public class VolunteerNeedAccessorFake : IVolunteerNeedAccessor
    {
        private List<VolunteerNeed> _fakeVolunteerNeeds = new List<VolunteerNeed>();
    
        public VolunteerNeedAccessorFake()
        {
            _fakeVolunteerNeeds.Add(new VolunteerNeed()
            {
                TaskID = 999999,
                NumTotalVolunteers = 1,
                NumCurrVolunteers = 0
            });

            _fakeVolunteerNeeds.Add(new VolunteerNeed()
            {
                TaskID = 999998,
                NumTotalVolunteers = 1,
                NumCurrVolunteers = 1
            });
        
            _fakeVolunteerNeeds.Add(new VolunteerNeed()
            {
                TaskID = 999997,
                NumTotalVolunteers = 0,
                NumCurrVolunteers = 0
            });
            _fakeVolunteerNeeds.Add(new VolunteerNeed()
            {
                TaskID = 999996,
                NumTotalVolunteers = 1,
                NumCurrVolunteers = 0
            });
        }
        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: removes a need
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int DeleteVolunteerNeed(int taskID)
        {
            int rowsAffected = 0;
            int count = _fakeVolunteerNeeds.Count;
            int newcount;
            VolunteerNeed need2 = new VolunteerNeed();
            try
            {
                foreach(var need in _fakeVolunteerNeeds)
                {
                    if (need.TaskID == taskID)
                    {
                        need2 = need;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            _fakeVolunteerNeeds.Remove(need2);
            newcount = _fakeVolunteerNeeds.Count;
            if (count > newcount)
            {
                rowsAffected = 1;
            }
            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: adds a need
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="numTotalVolunteers"></param>
        /// <returns></returns>
        public int InsertVolunteerNeed(int taskID, int numTotalVolunteers)
        {
            int rowsAffected = 0;

            try
            {
                _fakeVolunteerNeeds.Add(new VolunteerNeed()
                {
                    TaskID = 999995,
                    NumTotalVolunteers = 1,
                    NumCurrVolunteers = 0
                });
                rowsAffected++;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: selects need by task id
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public VolunteerNeed SelectVolunteerNeedByTaskID(int taskID)
        {
            VolunteerNeed selectedNeed = new VolunteerNeed();
            selectedNeed = null;
            foreach (var need in _fakeVolunteerNeeds)
            {
                if (need.TaskID == taskID)
                {
                    selectedNeed = need;
                }
            }
            return selectedNeed;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: increases the number of currvolunteers
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int UpdateAddCurrVolunteers(int taskID)
        {
            int rowsAffected = 0;

            foreach (var need in _fakeVolunteerNeeds)
            {
                if (need.TaskID == taskID && need.NumCurrVolunteers < need.NumTotalVolunteers)
                {
                    need.NumCurrVolunteers++;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: decreases the number of currvolunteers
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
        public int UpdateSubtractCurrVolunteers(int taskID)
        {
            int rowsAffected = 0;

            foreach (var need in _fakeVolunteerNeeds)
            {
                if (need.TaskID == taskID && need.NumCurrVolunteers > 0)
                {
                    need.NumCurrVolunteers--;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created: 2022/03/15
        /// 
        /// Description: changes numTotalVolunteers
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="numTotalVolunteers"></param>
        /// <returns></returns>
        public int UpdateVolunteerNeed(int taskID, int numTotalVolunteers)
        {
            int rowsAffected = 0;

            foreach (var need in _fakeVolunteerNeeds)
            {
                if (need.TaskID == taskID && numTotalVolunteers <= 10 && numTotalVolunteers >= 0)
                {
                    need.NumTotalVolunteers = numTotalVolunteers;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }
    }
}
