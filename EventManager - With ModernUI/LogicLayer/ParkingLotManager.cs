using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataObjects;
using LogicLayerInterfaces; 

namespace LogicLayer
{
    public class ParkingLotManager : IParkingLotManager
    {
        IParkingLotAccessor _parkingLotAccessor = null;


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Constructor for parking lot manager using the parking lot accessor
        /// </summary>
        public ParkingLotManager()
        {
            _parkingLotAccessor = new ParkingLotAccessor();
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Constructor for parking lot manager that takes an IParkingLotAccessor for testing purposes
        /// </summary>
        /// <param name="parkingLotAccessor">An object that implements IParkingLotAccessor</param>
        public ParkingLotManager(IParkingLotAccessor parkingLotAccessor)
        {
            _parkingLotAccessor = parkingLotAccessor;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Inserts a parking lot for a location
        /// </summary>
        /// <param name="parkingLot"></param>
        /// <returns></returns>
        public int CreateParkingLot(ParkingLot parkingLot)
        {
            int lotID = 0;
            // Green
            //lotID = 100005;

            if (parkingLot.LocationID == 0)
            {
                throw new ApplicationException("No Location to associate with the parking lot. Add a location.");
            }

            if (parkingLot.Name == "" || parkingLot.Name == null)
            {
                throw new ApplicationException("Please enter a name for the parking lot.");
            }

            if (parkingLot.Name.Length > 160)
            {
                throw new ApplicationException("The name of the parking lot is too long.");
            }

            if (parkingLot.Description != null && parkingLot.Description.Length > 3000)
            {
                throw new ApplicationException("The description of the parking lot is too long.");
            }

            try
            {
                lotID = _parkingLotAccessor.InsertParkingLot(parkingLot);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lotID;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Edits the selected Parking lot
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/15
        /// 
        /// Fixed a crash when passing a null description.
        /// </summary>
        /// <param name="lotID">ID of Parking lot being edited</param>
        /// <param name="oldParkingLot">Parking lot object before edit</param>
        /// <param name="newParkingLot">Parking lot object after edit</param>
        /// <returns>Number of rows affected. Should be one if successful</returns>
        public int EditParkingLotByLotID(int lotID, ParkingLot oldParkingLot, ParkingLot newParkingLot)
        {
            int rowsAffected = 0;

            if(newParkingLot.LocationID == 0)
            {
                throw new ApplicationException("No Location to associate with the parking lot. Add a location.");
            }
            if (newParkingLot.Name == "" || newParkingLot.Name == null)
            {
                throw new ApplicationException("Please enter a name for the parking lot.");
            }

            if (newParkingLot.Name.Length > 160)
            {
                throw new ApplicationException("The name of the parking lot is too long.");
            }

            if (newParkingLot.Description != null && newParkingLot.Description.Length > 3000)
            {
                throw new ApplicationException("The description of the parking lot is too long.");
            }

            try
            {
                rowsAffected = _parkingLotAccessor.UpdateParkingLotByLotID(lotID, oldParkingLot, newParkingLot);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Deletes parking lot
        /// </summary>
        /// <param name="lotID">The lot to delete</param>
        /// <returns>True is removed, false if not</returns>
        public bool RemoveParkingLotByLotID(int lotID)
        {
            bool result = false;

            try
            {
                result = _parkingLotAccessor.DeleteParkingLotByLotID(lotID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (!result)
            {
                throw new ApplicationException("The parking lot was not deleted");
            }

            return result;


        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Selects the ParkingLotVM for the event location
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<ParkingLotVM> RetrieveParkingLotByLocationID(int locationID)
        {
            List<ParkingLotVM> parkingLots = new List<ParkingLotVM>();

            // Green
            //parkingLots.Add(new ParkingLotVM());
            //parkingLots.Add(new ParkingLotVM());
            //parkingLots.Add(new ParkingLotVM());

            try
            {
                parkingLots = _parkingLotAccessor.SelectParkingLotByLocationID(locationID);
            }
            catch (Exception ex)
            {

                throw ex;
            }


            return parkingLots;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/11
        /// 
        /// Description:
        /// Retrieves a Parking lot VM based on Lot ID
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/13
        /// Removed exception causing issues in UI.
        /// </summary>
        /// <param name="lotID">ID of the selected Parking Lot</param>
        /// <returns>Parking lot object of the same lotID</returns>
        public ParkingLotVM RetrieveParkingLotByLotID(int lotID)
        {
            ParkingLotVM requestedParkingLot = null;

            try
            {
                requestedParkingLot = _parkingLotAccessor.SelectParkingLotByLotID(lotID);
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to retrieve parking lot.", ex);
            }

            return requestedParkingLot;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Checks to see if the user can edit the parking lot
        /// </summary>
        /// <param name="userID">The ID for the user</param>
        /// <returns>True if removed, false if not</returns>
        public bool UserCanEditParkingLot(int userID)
        {
            bool result = false;

            try
            {
                result = _parkingLotAccessor.UserCanEditParkingLot(userID);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }
    }
}
