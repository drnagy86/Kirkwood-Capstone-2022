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
    public class LocationManager : ILocationManager
    {
        ILocationAccessor _locationAccessor = null;

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Default constructor for location manager using the live location accessor
        /// </summary>
        public LocationManager()
        {
            _locationAccessor = new LocationAccessor();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Constructor for location manager using a given location accessor
        /// </summary>
        /// <param name="locationAccessor"></param>
        public LocationManager(ILocationAccessor locationAccessor)
        {
            _locationAccessor = locationAccessor;
        }

        
            /// <summary>
            /// Logan Baccam
            /// Created: 2022/01/25
            /// 
            /// Description:
            /// Creates a Location
            /// </summary>
            /// <param name="locationName"></param>
            /// <param name="address"></param>
            /// <param name="locationCity"></param>
            /// <param name="locationState"></param>
            /// <param name="locationZip"></param>
            /// <returns>Number of rows added</returns>
            public int CreateLocation(string locationName, string address, string locationCity, string locationState, string locationZip)
            {
                int rowsAffected = 0;
                if (locationName == "" || locationName == null)
                {
                    throw new ApplicationException("Location name can not be empty.");
                }
                if (locationName.Length > 160 || locationName.Length < 1)
                {
                    throw new ApplicationException("Invalid location name. Location name must be between 1-160 characters.");
                }
                if (address == "" || address == null)
                {
                    throw new ApplicationException("Location address can not be empty.");
                }
                if (address.Length > 100 || address.Length < 1)
                {
                    throw new ApplicationException("Invalid address. Address must be between 1-100 characters.");
                }
                if (locationCity == "" || locationCity == null)
                {
                    throw new ApplicationException("City can not be empty.");
                }
                if (locationCity.Length > 100 || locationCity.Length < 1)
                {
                    throw new ApplicationException("Invalid City name. City name must be between 1-100 characters.");
                }
                if (locationState == "" || locationState == null)
                {
                    throw new ApplicationException("State can not be empty.");
                }
                if (locationZip.Length > 100 || locationZip.Length < 1)
                {
                    throw new ApplicationException("Invalid zip code. Zip code must be between 1-100 characters.");
                }
                if (locationZip.Length > 100 || locationZip.Length < 1)
                {
                    throw new ApplicationException("Invalid Zip code. Zip code must be between 1-100 characters.");
                }
                try
                {
                    rowsAffected = _locationAccessor.InsertLocation(locationName, address, locationCity, locationState, locationZip);
                }
                catch (Exception ex) { throw ex; }

                return rowsAffected;
            }
        

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Retrieves all active locations from database
        /// </summary>
        /// <returns>List of all active locations</returns>
        public List<Location> RetrieveActiveLocations()
        {
            List<Location> locations = new List<Location>();

            try
            {
                locations = _locationAccessor.SelectActiveLocations();
            }
            catch (Exception)
            {
                throw;
            }

            return locations;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Method to retrieve a location by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A Location object</returns>
        public Location RetrieveLocationByLocationID(int locationID)
        {
            Location location = new Location();

            try
            {
                location = _locationAccessor.SelectLocationByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return location;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/25
        /// 
        /// Description:
        /// returns a matching LocationVM object
        /// </summary>
        /// <param name="locationName"></param>
        /// <param name="address"></param>
        /// <returns>A matching location record</returns>
        public Location RetrieveLocationByNameAndAddress(string locationName, string address)
        {
            Location _matchingLocation = new Location();
            if (locationName == "" || locationName == null)
            {
                throw new ApplicationException("Location name can not be empty.");
            }
            if (locationName.Length > 160 || locationName.Length < 1)
            {
                throw new ApplicationException("Invalid location name. Location name must be between 1-160 characters.");
            }
            if (address == "" || address == null)
            {
                throw new ApplicationException("Location address can not be empty.");
            }
            if (address.Length > 100 || address.Length < 1)
            {
                throw new ApplicationException("Invalid location name. Location name must be between 1-100 characters.");
            }
            try
            {
                _matchingLocation = _locationAccessor.SelectLocationByLocationNameAndAddress(locationName, address);
            }
            catch (Exception ex) { throw ex; }

            return _matchingLocation;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Method to retrieve location images by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationImage objects</returns>
        public List<LocationImage> RetrieveLocationImagesByLocationID(int locationID)
        {
            List<LocationImage> locationImages = new List<LocationImage>();

            try
            {
                locationImages = _locationAccessor.SelectLocationImagesByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return locationImages;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Method to retrieve location reviews by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationReview objects</returns>
        public List<Reviews> RetrieveLocationReviews(int locationID)
        {
            List<Reviews> locationReviews = new List<Reviews>();

            try
            {
                locationReviews = _locationAccessor.SelectLocationReviews(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return locationReviews;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Method to deactivate a location by its locationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>int number of rows affected</returns>
        public int DeactivateLocationByLocationID(int locationID)
        {
            int rowsAffected = 0;

            try
            {
                rowsAffected = _locationAccessor.DeactivateLocationByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Retrieve location availability on a given date by LocationID 
        /// First tries to get any availability exceptions for the given date.
        /// If it fails to find any, then it retrieves the regular weekly availability.
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="date"></param>
        /// <returns>A list of Availability objects</returns>
        public List<Availability> RetrieveLocationAvailabilityByLocationIDAndDate(int locationID, DateTime date)
        {
            List<Availability> locationAvailabilities = new List<Availability>();

            try
            {
                locationAvailabilities = _locationAccessor.SelectLocationAvailabilityExceptionByLocationIDAndDate(locationID, date);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve location availability exceptions", ex);
            }

            // if failed to find any exceptions, get regular weekly availability
            if (locationAvailabilities.Count == 0)
            {
                try
                {
                    locationAvailabilities = _locationAccessor.SelectLocationAvailabilityByLocationIDAndDate(locationID, date);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Failed to retrieve location availability", ex);
                }
            }

            return locationAvailabilities;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Method to update a location description by its locationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>int number of rows affected</returns>
        public int UpdateLocationBioByLocationID(Location oldLocation, Location newLocation)
        {
            int rowsAffected = 0;
            if (newLocation.Description != null && newLocation.Description.Length > 3000) // desription too long (description can be null)
            {
                throw new ApplicationException("Description too long");
            }
            if (newLocation.Phone != null && newLocation.Phone != "") // phone number format validation (phone can be null)
            {
                if (!WPFPresentation.ValidationHelpers.IsValidPhone(newLocation.Phone))
                    throw new ApplicationException("Invalid phone number");
            }
            if (newLocation.Email != null && newLocation.Email != "") // email format validation (email can be null)
            {
                if (!WPFPresentation.ValidationHelpers.IsValidEmailAddress(newLocation.Email))
                    throw new ApplicationException("Invalid email address");
            }
            if (newLocation.Address1 == "" || newLocation.Address1 == null) // no address one
            {
                throw new ApplicationException("Please enter an address");
            }
            if (newLocation.Address1.Length > 100) // address one too long 
            {
                throw new ApplicationException("Address one cannot be longer than 100 characters");
            }
            if (newLocation.Address2 != null && newLocation.Address2 != "" 
                && newLocation.Address2.Length > 100) // address two too long (address two can be null)
            {
                throw new ApplicationException("Address two cannot be longer than 100 characters"); //3000
            }
            if (newLocation.PricingInfo != null && newLocation.PricingInfo != "" &&
                newLocation.PricingInfo.Length > 3000) // pricing too long (pricing can be null)
            {
                throw new ApplicationException("Pricing cannot be longer than 3000 characters");
            }
            try
            {
                rowsAffected = _locationAccessor.UpdateLocationBioByLocationID(oldLocation, newLocation);
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Retrieve location availability by LocationID 
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of AvailabilityVM objects</returns>
        public List<AvailabilityVM> RetrieveLocationAvailabilityByLocationID(int locationID)
        {
            List<AvailabilityVM> availabilities = new List<AvailabilityVM>();

            try
            {
                availabilities = _locationAccessor.SelectLocationAvailabilityByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return availabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Retrieve location availability exceptions by LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of Availability objects</returns>
        public List<Availability> RetrieveLocationAvailabilityExceptionByLocationID(int locationID)
        {
            List<Availability> availabilities = new List<Availability>();

            try
            {
                availabilities = _locationAccessor.SelectLocationAvailabilityExceptionByLocationID(locationID);
            }
            catch (Exception)
            {

                throw;
            }

            return availabilities;
		}

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/03
        /// 
        /// Description:
        /// returns a list of tags associated with the location
        /// 
        /// </summary>
        /// <param name="locationID"
        /// <returns>List of all active locations</returns>
        public List<string> RetrieveTagsByLocationID(int locationID)
        {
            List<string> tags = new List<string>();
            try
            {
                tags = _locationAccessor.SelectTagsbyLocationID(locationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return tags;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/22
        /// 
        /// Description:
        /// Creates a location review in the database or throws an error if the review is invalid
        /// </summary>
        /// <param name="review"></param>
        /// <returns>rows affected</returns>
        public int CreateLocationReview(Reviews review)
        {
            try
            {
                if (review.Rating < 1 || review.Rating > 5)
                {
                    throw new ArgumentException("Rating must be between 1 and 5.");
                }
                else if (review.Review.Length > 3000)
                {
                    throw new ArgumentException("Please keep review under 3000 characters.");
                }
                return _locationAccessor.InsertLocationReview(review);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
