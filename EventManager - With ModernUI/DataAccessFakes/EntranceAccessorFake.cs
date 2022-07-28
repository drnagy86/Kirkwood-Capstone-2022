using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class EntranceAccessorFake : IEntranceAccessor
    {

        private List<Entrance> _fakeEntrances = new List<Entrance>();

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Constructor that adds fake entrances to _fakeEntrances list for tesing purposes
        /// </summary>
        public EntranceAccessorFake()
        {
            _fakeEntrances.Add(new Entrance()
            {
                EntranceID = 100000,
                LocationID = 100000,
                EntranceName = "Test Entrance 1",
                Description = "A description of test entrance 1"
            });
            _fakeEntrances.Add(new Entrance()
            {
                EntranceID = 100001,
                LocationID = 100000,
                EntranceName = "Test Entrance 2",
                Description = "A description of test entrance 2"
            });
            _fakeEntrances.Add(new Entrance()
            {
                EntranceID = 100002,
                LocationID = 100001,
                EntranceName = "Test Entrance 3",
                Description = "A description of test entrance 3"
            });
            _fakeEntrances.Add(new Entrance()
            {
                EntranceID = 100003,
                LocationID = 100002,
                EntranceName = "Test Entrance 4",
                Description = "A description of test entrance 4"
            });
        }

        /// <summary>
        /// Logan Baccam
        /// Created 2022/03/06
        /// 
        /// Description:
        /// method that deactivates an entrance from the _fakeEntrances List by setting active to false
        /// <param name="entranceID"/>
        /// </summary>
        public int DeactivateEntranceByEntranceID(int entranceID)
        {
            int rows = 0;
            if (entranceID <= 99999)
            {
                throw new ArgumentException("Invalid entrance");
            }
            foreach (Entrance entrance in _fakeEntrances)
            {
                if (entrance.EntranceID == entranceID)
                {
                    entrance.Active = false;
                    rows++;
                }
            }

            return rows;
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Insert fake entrance for testing
        /// 
        /// </summary>
        /// <param name="locationID">ID of associated location</param>
        /// <param name="entranceName">Name of entrance location</param>
        /// <param name="description">Description of entrance, may include directions</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertEntrance(int locationID, string entranceName, string description)
        {
            int rowsAffected = 0;
            int entranceID = _fakeEntrances.Last().EntranceID + 1;


            _fakeEntrances.Add(new Entrance()
            {
                EntranceID = entranceID,
                LocationID = locationID,
                EntranceName = entranceName,
                Description = description
            });

            rowsAffected++;

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Returns all Entrances in a fake list of entrances that match the location ID passed
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<Entrance> SelectEntranceByLocationID(int locationID)
        {
            List<Entrance> entrances = new List<Entrance>();

            foreach(Entrance entrance in _fakeEntrances)
            {
                if(entrance.LocationID == locationID)
                {
                    entrances.Add(entrance);
                }
            }

            return entrances;
        }

        public int UpdateEntrance(Entrance oldEntrance, Entrance newEntrance)
        {
            int rowsAffected = 0;

            foreach(var fakeEntrance in _fakeEntrances)
            {
                if(fakeEntrance.EntranceID == oldEntrance.EntranceID 
                    && fakeEntrance.EntranceName.Equals(oldEntrance.EntranceName)
                    && fakeEntrance.Description.Equals(oldEntrance.Description))
                {
                    fakeEntrance.EntranceName = newEntrance.EntranceName;
                    fakeEntrance.Description = newEntrance.Description;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }
    }
}
