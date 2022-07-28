using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface IParkingLotAccessor
    {
        int InsertParkingLot(ParkingLot parkingLot);
        int UpdateParkingLotByLotID(int lotID, ParkingLot oldParkingLot, ParkingLot newParkingLot);
        List<ParkingLotVM> SelectParkingLotByLocationID(int locationID);
        bool DeleteParkingLotByLotID(int lotID);
        bool UserCanEditParkingLot(int userID);
        ParkingLotVM SelectParkingLotByLotID(int lotID);
    }
}
