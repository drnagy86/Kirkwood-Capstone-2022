using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace DataAccessInterfaces
{
    public interface ILocationAccessor
    {
        List<Location> SelectActiveLocations();
        Location SelectLocationByLocationID(int locationID);
        List<Reviews> SelectLocationReviews(int locationID);
        int InsertLocationReview(Reviews review);
        List<LocationImage> SelectLocationImagesByLocationID(int locationID);
        int InsertLocation(string locationName, string address, string locationCity, string locationState, string locationZipCode);
        Location SelectLocationByLocationNameAndAddress(string locationName, string address);
        int DeactivateLocationByLocationID(int locationID);
        int UpdateLocationBioByLocationID(Location oldLocation, Location newLocation);
        List<Availability> SelectLocationAvailabilityByLocationIDAndDate(int locationID, DateTime date);
        List<Availability> SelectLocationAvailabilityExceptionByLocationIDAndDate(int locationID, DateTime date);
        List<AvailabilityVM> SelectLocationAvailabilityByLocationID(int locationID);
        List<Availability> SelectLocationAvailabilityExceptionByLocationID(int locationID);
        List<string> SelectTagsbyLocationID(int locationID);
    }
}
