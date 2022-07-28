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
    public class EntranceManager : IEntranceManager
    {

        IEntranceAccessor _entranceAccessor = null;

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Constructor for entrance manager using the entrance accessor
        /// </summary>
        public EntranceManager()
        {
            _entranceAccessor = new EntranceAccessor();
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Constructor that takes an IEntranceAccessor and sets it to the _entranceAccessor field for passing test data
        /// </summary>
        /// <param name="entranceAccessor"></param>
        public EntranceManager(IEntranceAccessor entranceAccessor)
        {
            _entranceAccessor = entranceAccessor;
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Creates an entrance
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="entranceName"></param>
        /// <param name="description"></param>
        /// <returns>Number of rows added</returns>
        public int CreateEntrance(int locationID, string entranceName, string description)
        {
            int rowsAffected = 0;


            if (entranceName == "" || entranceName == null)
            {
                throw new ApplicationException("Name can not be empty.");
            }
            if (entranceName.Length > 100)
            {
                throw new ApplicationException("Name can not be over 100 characters.");
            }

            if (description == "" || description == null)
            {
                throw new ApplicationException("Description can not empty.");
            }
            if (description.Length >= 255)
            {
                throw new ApplicationException("Description can not over 255 characters.");
            }


            try
            {
                rowsAffected = _entranceAccessor.InsertEntrance(locationID, entranceName, description);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Logan Baccam
        /// Created 2022/03/06
        /// 
        /// Description:
        /// method that deactivates an entrance from the Entrance table.
        /// <param name="entranceID"/>
        /// </summary>
        public int RemoveEntranceByEntranceID(int entranceID)
        {
            int rows = 0;
            if (entranceID <= 99999)
            {
                throw new ArgumentException("Invalid entrance");
            }
            try
            {
                rows = _entranceAccessor.DeactivateEntranceByEntranceID(entranceID);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Something went wrong when removing this entrance.");
            }
            return rows;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Method that retrieves all entrances for a location by it's LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<Entrance> RetrieveEntranceByLocationID(int locationID)
        {
            List<Entrance> entrances = new List<Entrance>();

            try
            {
                entrances = _entranceAccessor.SelectEntranceByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return entrances;
        }

        public bool UpdateEntrance(Entrance oldEntrance, Entrance newEntrance)
        {
            bool result = false;

            if (newEntrance.EntranceName == "" || newEntrance.EntranceName == null)
            {
                throw new ApplicationException("Name can not be empty.");
            }
            if (newEntrance.EntranceName.Length > 100)
            {
                throw new ApplicationException("Name can not be over 100 characters.");
            }

            if (newEntrance.Description == "" || newEntrance.Description == null)
            {
                throw new ApplicationException("Description can not empty.");
            }

            if (newEntrance.Description.Length > 255)
            {
                throw new ApplicationException("Description can not over 255 characters.");
            }

            try
            {
                result = 1 == _entranceAccessor.UpdateEntrance(oldEntrance, newEntrance);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}
