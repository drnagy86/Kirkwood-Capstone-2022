using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class ParkingLotAccessorFake : IParkingLotAccessor
    {
        private List<ParkingLotVM> _fakeParkingLots = new List<ParkingLotVM>();
        private Dictionary<User, Role> _fakeUserRoles = new Dictionary<User, Role>();

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Constructor that adds fake parking lots to a list for tesing purposes
        /// 
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Added fake user roles
        /// 
        /// </summary>
        public ParkingLotAccessorFake()
        {
            addFakeUserRoles();

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100000,
                LocationID = 100000,
                Name = "Test Parking Lot A",
                Description = "A description for test parking lot A",
                ImageName = null,
                LocationName = "Test Location"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100001,
                LocationID = 100000,
                Name = "Test Parking Lot B",
                Description = "A description for test parking lot B",
                ImageName = null,
                LocationName = "Test Location"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100002,
                LocationID = 100000,
                Name = "Test Parking Lot C",
                Description = "A description for test parking lot C",
                ImageName = null,
                LocationName = "Test Location"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100003,
                LocationID = 100001,
                Name = "Test Parking Lot For Location 2",
                Description = "A description for test parking lot 4 for Test location 2",
                ImageName = null,
                LocationName = "Test Location 2"
            });

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = 100004,
                LocationID = 100002,
                Name = "Test Parking Lot For Location 3",
                Description = "A description for test parking lot 5 for Test location 3",
                ImageName = null,
                LocationName = "Test Location 3"
            });

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Inserts a fake parking lot object
        /// </summary>
        /// <param name="parkingLot"></param>
        /// <returns>The inserted parking lot id</returns>
        public int InsertParkingLot(ParkingLot parkingLot)
        {
            int lotID = 0;
            lotID = nextAvailableLotID();

            _fakeParkingLots.Add(new ParkingLotVM()
            {
                LotID = lotID,
                LocationID = parkingLot.LocationID,
                Name = parkingLot.Name,
                Description = parkingLot.Description,
                ImageName = parkingLot.ImageName
            });

            return lotID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Selects the list of parking lots for the location ID
        /// </summary>
        /// <param name="locationID">The locationID</param>
        /// <returns>The ParkingLotVMs for the location </returns>
        public List<ParkingLotVM> SelectParkingLotByLocationID(int locationID)
        {
            return _fakeParkingLots.FindAll(pl => pl.LocationID == locationID);
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
        public bool DeleteParkingLotByLotID(int lotID)
        {
            bool result = false;

            foreach (var lot in _fakeParkingLots)
            {
                if (lot.LotID == lotID)
                {
                    _fakeParkingLots.Remove(lot);
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Checks to see if the fake user can edit the fake parking lot
        /// </summary>
        /// <param name="userID">The ID for the user</param>
        /// <returns>True if removed, false if not</returns>
        public bool UserCanEditParkingLot(int userID)
        {
            bool result = false;

            foreach (var userRole in _fakeUserRoles)
            {
                if (userRole.Key.UserID == userID && userRole.Value.RoleID == "Event Planner" )
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/01
        /// 
        /// Description:
        /// Gets the next available fake lot id
        /// </summary>
        /// <param name="parkingLot"></param>
        /// <returns>The inserted parking lot id</returns>
        private int nextAvailableLotID()
        {
            int lotID = 0;

            lotID = _fakeParkingLots[_fakeParkingLots.Count - 1].LotID + 1;

            return lotID;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/08
        /// 
        /// Description:
        /// Sets up dictionary for fake roles for the user
        /// </summary>
        private void addFakeUserRoles()
        {
            _fakeUserRoles.Add(new User { UserID = 100000 }, new Role { RoleID = "Event Planner" });
            _fakeUserRoles.Add(new User { UserID = 100001 }, new Role { RoleID = "Test" });
            _fakeUserRoles.Add(new User { UserID = 100002 }, new Role { RoleID = "Test" });
            _fakeUserRoles.Add(new User { UserID = 100003 }, new Role { RoleID = "Event Planner" });
            _fakeUserRoles.Add(new User { UserID = 100004 }, new Role { RoleID = "Attendee" });
            _fakeUserRoles.Add(new User { UserID = 100000 }, new Role { RoleID = "Attendee" });
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/09
        /// 
        /// Description:
        /// Updates a fake Parking lot by lotID
        /// </summary>
        /// <param name="lotID"></param>
        /// <param name="oldParkingLot"></param>
        /// <param name="newParkingLot"></param>
        /// <returns></returns>
        public int UpdateParkingLotByLotID(int lotID, ParkingLot oldParkingLot, ParkingLot newParkingLot)
        {
            int rowsAffected = 0;

            foreach(var fakeLots in _fakeParkingLots)
            {
                if(fakeLots.LotID == newParkingLot.LotID && fakeLots.LocationID == oldParkingLot.LocationID
                    && fakeLots.Name == oldParkingLot.Name && fakeLots.Description == oldParkingLot.Description
                    && fakeLots.ImageName == oldParkingLot.ImageName)
                {
                    fakeLots.LocationID = newParkingLot.LocationID;
                    fakeLots.Name = newParkingLot.Name;
                    fakeLots.Description = newParkingLot.Description;
                    fakeLots.ImageName = newParkingLot.ImageName;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/11
        /// 
        /// Description:
        /// Method that selects a parking lot out of a list of fakes by lot ID
        /// 
        /// Christopher Repko
        /// Updated: 2022/04/13
        /// Removed exception causing issues in UI.
        /// </summary>
        /// <param name="lotID"></param>
        /// <returns></returns>
        public ParkingLotVM SelectParkingLotByLotID(int lotID)
        {
            ParkingLotVM parkingLot = null;

            foreach(var fakeParkingLot in _fakeParkingLots)
            {
                if(fakeParkingLot.LotID == lotID)
                {
                    parkingLot = fakeParkingLot;
                }
            }

            return parkingLot;
        }
    }
}
