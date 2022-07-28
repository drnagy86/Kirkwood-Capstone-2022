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

    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/02/22
    /// 
    /// Description:
    /// Class for handling Sublocation manager methods
    /// </summary>
    public class SublocationManager : ISublocationManager
    {
        ISublocationAccessor _sublocationAccessor = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Default constructor for sublocation manager using the live sublocation accessor
        /// </summary>
        public SublocationManager()
        {
            _sublocationAccessor = new SublocationAccessor();
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Constructor for sublocation manager using a given sublocation accessor
        /// </summary>
        /// <param name="sublocationAccessor"></param>
        public SublocationManager(ISublocationAccessor sublocationAccessor)
        {
            _sublocationAccessor = sublocationAccessor;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/02/26
        /// 
        /// Description:
        /// Inserts a sublocation into the sublocation table
        ///
        /// </summary>
        /// <param name="sublocationID"></param>
        /// <param name="sublocationName"></param>
        /// <param name="sublocationDescription"></param>
        /// <returns>the rows affected</returns>
        public int CreateSublocationByLocationID(int locationID, string sublocationName, string sublocationDesc)
        {
            int rows = 0;
            if (locationID >= 100000 && locationID <= 999999)
            {
                if (sublocationDesc != null && sublocationDesc.Length >= 3001)
                {
                    throw new ArgumentException("Description cannot exceed 3000 characters.");
                }
                if (sublocationName.Length >= 161 && sublocationName.Length <= 0)
                {
                    throw new ArgumentException("Name must be between 1-160 characters.");

                }
                try
                {
                    rows = _sublocationAccessor.InsertSublocationByLocationID(locationID, sublocationName, sublocationDesc);

                }
                catch (Exception ex)
                { throw ex; }
            }
            else
            {
                throw new ArgumentException("Invalid LocationID.");
            }
            return rows;

        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Deactivates a sublocation from sublocation table.
        /// 
        /// </summary>
        /// <param name="sublocationID">ID of sublocation to be deactivated</param>
        /// <returns></returns>
        public int DeactivateSublocationBySublocationID(int sublocationID)
        {
            int result = 0;
            try
            {
                result = _sublocationAccessor.DeactivateSublocationBySublocationID(sublocationID);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to deactivate sublocation", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Replaces one sublocation with another.
        /// 
        /// </summary>
        /// <param name="oldSublocation">Sublocation to replace</param>
        /// <param name="newSublocation">Sublocation to replace with</param>
        /// <returns>Integer value representing number of rows affected.</returns>
        public int EditSublocationBySublocationID(Sublocation oldSublocation, Sublocation newSublocation)
        {
            int result = 0;
            try
            {
                result = _sublocationAccessor.UpdateSublocation(oldSublocation, newSublocation);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to update sublocation", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Retrieves a sublocation based on a sublocationID
        /// </summary>
        /// <param name="sublocationID">SublocationID to retrieve the sublocation for</param>
        /// <returns>A sublocation matching the sublocationID passed in.</returns>
        public Sublocation RetrieveSublocationBySublocationID(int sublocationID)
        {
            Sublocation result = null;
            try
            {
                result = this._sublocationAccessor.SelectSublocationBySublocationID(sublocationID);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to retrieve sublocation", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Retrieves a list of sublocations based on a locationID
        /// </summary>
        /// <param name="locationID">LocationID to retrieve sublocations matching.</param>
        /// <returns>A list of sublocations matching the locationID passed in.</returns>
        public List<Sublocation> RetrieveSublocationsByLocationID(int locationID)
        {
            List<Sublocation> result = new List<Sublocation>();
            try
            {
                result = this._sublocationAccessor.SelectSublocationsByLocationID(locationID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve list of sublocations for location", ex);
            }
            return result;
        }
    }
}
