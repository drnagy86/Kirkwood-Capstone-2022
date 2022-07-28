using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class LocationAccessorFake : ILocationAccessor
    {
        private List<Location> _fakeLocations = new List<Location>();
        private List<Reviews> _fakeLocationReviews = new List<Reviews>();
        private List<LocationImage> _fakeLocationImages = new List<LocationImage>();
        private List<LocationAvailabilityTableFake> _dbFake = new List<LocationAvailabilityTableFake>();
        private Dictionary<int, List<string>> _tags = new Dictionary<int, List<string>>();

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Constructor to populate _fakeLocations with dummy values for testing purposes
        /// 
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/03/23
        /// 
        /// Description:
        /// Copy and pasted the fake location data into the Event Accessor Fake
        /// 
        /// </summary>
        public LocationAccessorFake()
        {
            _fakeLocations.Add(new Location()
            {
                LocationID = 100000,
                UserID = 100000,
                Name = "Test Location 1",
                Description = "Description of Test Location 1 goes here.",
                PricingInfo = "Pricing information for renting Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                City = "Iowa City",
                State = "Iowa",
                ZipCode = "52240",
                ImagePath = "http://imagehost.com/testlocation1.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100001,
                UserID = 100000,
                Name = "Test Location 2",
                Description = "Description of Test Location 2 goes here.",
                PricingInfo = "Pricing information for renting Test Location 2 goes here.",
                Phone = "222-222-2222",
                Email = "testLocation2@locations.com",
                Address1 = "Test Location 2 Street",
                Address2 = "Apt 2",
                City = "Cedar Rapids",
                State = "Iowa",
                ZipCode = "52404",
                ImagePath = "http://imagehost.com/testlocation2.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100002,
                UserID = 100000,
                Name = "Test Location 3",
                Description = "Description of Test Location 3 goes here.",
                PricingInfo = "Pricing information for renting Test Location 3 goes here.",
                Phone = "333-333-3333",
                Email = "testLocation3@locations.com",
                Address1 = "Test Location 3 Street",
                Address2 = "Apt 33",
                City = "Chicago",
                State = "Illinois",
                ZipCode = "60007",
                ImagePath = "http://imagehost.com/testlocation3.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100003,
                UserID = 100000,
                Name = "Test Location 4",
                Description = "Description of Test Location 4 goes here.",
                PricingInfo = "Pricing information for renting Test Location 4 goes here.",
                Phone = "444-444-4444",
                Email = "testLocation4@locations.com",
                Address1 = "Test Location 4 Street",
                Address2 = "Apt 44",
                City = "New York City",
                State = "New York",
                ZipCode = "10036",
                ImagePath = "http://imagehost.com/testlocation4.png",
                Active = true
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100004,
                UserID = 100000,
                Name = "Test Location 5 Inactive",
                Description = "Description of Inactive Test Location 5 goes here.",
                PricingInfo = "Pricing information for renting inactive Test Location 5 goes here.",
                Phone = "555-555-5555",
                Email = "testLocation5@locations.com",
                Address1 = "Test Location 5 Street",
                Address2 = "Apt 55",
                City = "Detroit",
                State = "Michigan",
                ZipCode = "48202",
                ImagePath = "http://imagehost.com/testlocation5.png",
                Active = false
            });

            _fakeLocations.Add(new Location()
            {
                LocationID = 100005,
                Name = "Test Location 6 Min Info",
                Address1 = "Test Location 6 Street",
                City = "Detroit",
                State = "Michigan",
                ZipCode = "48202",
                Active = true
            });

            _fakeLocationReviews.Add(new Reviews()
            {
                ForeignID = 100000,
                ReviewID = 100000,
                FullName = "Tess Data",
                ReviewType = "Location Review",
                Rating = 5,
                Review = "Great place",
                DateCreated = DateTime.Now,
                Active = true
            });

            _fakeLocationReviews.Add(new Reviews()
            {
                ForeignID = 100000,
                ReviewID = 100001,
                FullName = "Joey Speed",
                ReviewType = "Location Review",
                Rating = 2,
                Review = "Not a good place one bit",
                DateCreated = DateTime.Now,
                Active = true
            });

            _fakeLocationImages.Add(new LocationImage()
            {
                LocationID = 100000,
                ImageName = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            });

            _fakeLocationImages.Add(new LocationImage()
            {
                LocationID = 100000,
                ImageName = "7263a839-3428-49f2-b26f-875d3811ef85.jpg"
            });

            _dbFake.Add(new LocationAvailabilityTableFake()
            {
                Date = new DateTime(2022, 01, 01),
                Availabilities = new List<Availability>()
                {
                    new Availability()
                    {
                        ForeignID = 100000,
                        AvailabilityID = 100000,
                        TimeStart = new DateTime(2022, 01, 01, 8, 00, 00),
                        TimeEnd = new DateTime(2022, 01, 01, 11, 00, 00),
                    },
                    new Availability()
                    {
                        ForeignID = 100000,
                        AvailabilityID = 100001,
                        TimeStart = new DateTime(2022, 01, 01, 13, 00, 00),
                        TimeEnd = new DateTime(2022, 01, 01, 21, 00, 00)
                    }
                },
                IsException = false
            });

            _dbFake.Add(new LocationAvailabilityTableFake()
            {
                Date = new DateTime(2022, 01, 02),
                Availabilities = new List<Availability>()
                {
                    new Availability()
                    {
                        ForeignID = 100000,
                        AvailabilityID = 100002,
                        TimeStart = new DateTime(2022, 01, 02, 5, 30, 00),
                        TimeEnd = new DateTime(2022, 01, 02, 8, 30, 00)
                    }
                },
                IsException = false
            });

            _dbFake.Add(new LocationAvailabilityTableFake()
            {
                Date = new DateTime(2022, 01, 01),
                Availabilities = new List<Availability>()
                {
                    new Availability()
                    {
                        ForeignID = 100001,
                        AvailabilityID = 100003,
                        TimeStart = new DateTime(2022, 01, 01, 22, 15, 00),
                        TimeEnd = new DateTime(2022, 01, 01, 23, 00, 00)
                    }
                },
                IsException = false
            });

            _dbFake.Add(new LocationAvailabilityTableFake()
            {
                Date = new DateTime(2022, 01, 01),
                Availabilities = new List<Availability>()
                {
                    new Availability()
                    {
                        ForeignID = 100001,
                        AvailabilityID = 100004,
                        TimeStart = new DateTime(2022, 01, 01, 2, 45, 00),
                        TimeEnd = new DateTime(2022, 01, 01, 4, 45, 00)
                    }
                },
                IsException = true
            });

            _dbFake.Add(new LocationAvailabilityTableFake()
            {
                Date = new DateTime(2022, 01, 03),
                Availabilities = new List<Availability>()
                {
                    new Availability()
                    {
                        ForeignID = 100000,
                        AvailabilityID = 100005
                    }
                },
                IsException = true
            });

            List<string> tags = new List<string>();
            tags.Add("Location");
            tags.Add("Outdoor");
            tags.Add("Indoor");
            _tags.Add(100000, tags);
        
    }



        private class LocationAvailabilityTableFake
        {
            public DateTime Date { get; set; }
            public List<Availability> Availabilities { get; set; }
            public bool IsException { get; set; }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Returns all active locations in fake location list
        /// </summary>
        /// 
        /// <returns>List of active locations</returns>
        public List<Location> SelectActiveLocations()
        {
            List<Location> locations = new List<Location>();

            foreach (Location fakeLocation in _fakeLocations)
            {
                if (fakeLocation.Active)
                {
                    locations.Add(fakeLocation);
                }
            }

            return locations;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// The method that selects a location by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A Location object that matches the provided locationID or a null location if not there</returns>
        public Location SelectLocationByLocationID(int locationID)
        {
            Location location = new Location();

            foreach (Location fakeLocation in _fakeLocations)
            {
                if (fakeLocation.LocationID == locationID)
                {
                    location = fakeLocation;
                }
            }

            return location;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// The method that selects a location images by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of LocationImage object that matches the provided locationID or a null location if not there</returns>
        public List<LocationImage> SelectLocationImagesByLocationID(int locationID)
        {
            List<LocationImage> locationImages = new List<LocationImage>();

            foreach (LocationImage fakeLocationImage in _fakeLocationImages)
            {
                if (fakeLocationImage.LocationID == locationID)
                {
                    locationImages.Add(fakeLocationImage);
                }
            }

            return locationImages;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// The method that selects a location reviews by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of location review objects</returns>
        public List<Reviews> SelectLocationReviews(int locationID)
        {
            List<Reviews> locationReviews = new List<Reviews>();

            foreach (Reviews fakeLocationReview in _fakeLocationReviews)
            {
                if (fakeLocationReview.ForeignID == locationID)
                {
                    locationReviews.Add(fakeLocationReview);
                }
            }

            return locationReviews;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Insert fake location for testing
        /// 
        /// </summary>
        /// <param name="LocationName">Name of the event</param>
        /// <param name="address">name of the address</param>
        /// <param name="locationCity">address of the location</param>
        /// <param name="locationState">address of the location</param>
        /// <param name="locationZipCode">address of the location</param>
        /// <returns>Number of rows inserted</returns>
        public int InsertLocation(string locationName, string address, string locationCity, string locationState, string locationZip)
        {
            int rowsAffected = 0;
            _fakeLocations.Add(new Location()
            {
                Name = locationName,
                Address1 = address,
                City = locationCity,
                State = locationState,
                ZipCode = locationZip
            });

            rowsAffected++;

            return rowsAffected;
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Select matching location for testing
        /// 
        /// </summary>
        /// <param name="LocationName">Name of the location</param>
        /// <param name="address">Name of the address/param>
        /// <returns>matching location if it exists</returns>
        public Location SelectLocationByLocationNameAndAddress(string locationName, string address)
        {
            Location _eventLocation = new Location();
            _eventLocation.Name = locationName;
            _eventLocation.Address1 = address;

            foreach (Location location in _fakeLocations)
            {
                if (location.Name == _eventLocation.Name &&
                    location.Address1 == _eventLocation.Address1)
                {
                    _eventLocation = location;
                }

            }
            return _eventLocation;

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// The method that deactivates a location by its LocationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>int number of rows affected</returns>
        public int DeactivateLocationByLocationID(int locationID)
        {
            int rowsAffected = 0;

            foreach (Location fakeLocation in _fakeLocations)
            {
                if (fakeLocation.LocationID == locationID)
                {
                    fakeLocation.Active = false;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given locationID and date.
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Location on a given date</returns>
        public List<Availability> SelectLocationAvailabilityByLocationIDAndDate(int locationID, DateTime date)
        {
            List<Availability> availabilities = new List<Availability>();

            foreach (LocationAvailabilityTableFake fake in _dbFake)
            {
                if (fake.Date == date && !fake.IsException)
                {
                    foreach (Availability a in fake.Availabilities)
                    {
                        if (a.ForeignID == locationID)
                        {
                            availabilities.Add(a);
                        }
                    }
                }
            }

            return availabilities;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given locationID and date.
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Location on a given date</returns>
        public List<Availability> SelectLocationAvailabilityExceptionByLocationIDAndDate(int locationID, DateTime date)
        {
            List<Availability> availabilities = new List<Availability>();

            foreach (LocationAvailabilityTableFake fake in _dbFake)
            {
                if (fake.Date == date && fake.IsException)
                {
                    foreach (Availability a in fake.Availabilities)
                    {
                        if (a.ForeignID == locationID)
                        {
                            availabilities.Add(a);
                        }
                    }
                }
            }

            return availabilities;
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Updates a fake location bio information (description, phone, email, address, pricing)
        /// in the fake location list
        /// 
        /// </summary>
        public int UpdateLocationBioByLocationID(Location oldLocation, Location newLocation)
        {
            int rowsAffected = 0;

            foreach (Location fakeLocation in _fakeLocations)
            {
                if (fakeLocation.LocationID == oldLocation.LocationID 
                    && (fakeLocation.Description == oldLocation.Description)
                    && fakeLocation.Phone == oldLocation.Phone
                    && fakeLocation.Email == oldLocation.Email
                    && fakeLocation.Address1 == oldLocation.Address1
                    && fakeLocation.Address2 == oldLocation.Address2
                    && fakeLocation.PricingInfo == oldLocation.PricingInfo)
                {
                    fakeLocation.Description = newLocation.Description;
                    fakeLocation.Phone = newLocation.Phone;
                    fakeLocation.Email = newLocation.Email;
                    fakeLocation.Address1 = newLocation.Address1;
                    fakeLocation.Address2 = newLocation.Address2;
                    fakeLocation.PricingInfo = newLocation.PricingInfo;
                    rowsAffected++;
                }
            }

            return rowsAffected;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given locationID;
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of availability objects for a Location</returns>
        public List<AvailabilityVM> SelectLocationAvailabilityByLocationID(int locationID)
        {
            List<AvailabilityVM> availabilities = new List<AvailabilityVM>();

            try
            {
                foreach (LocationAvailabilityTableFake fake in _dbFake)
                {
                    if (!fake.IsException)
                    {
                        foreach (Availability a in fake.Availabilities)
                        {
                            if (a.ForeignID == locationID)
                            {
                                switch (fake.Date.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = locationID,
                                            Monday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Tuesday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = locationID,
                                            Tuesday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Wednesday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = locationID,
                                            Wednesday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Thursday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = locationID,
                                            Thursday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Friday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = locationID,
                                            Monday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Saturday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = locationID,
                                            Monday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Sunday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = locationID,
                                            Monday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                    }
                }
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
        /// Select one-off availability exception records matching the given locationID
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns>A list of availability objects for a Location</returns>
        public List<Availability> SelectLocationAvailabilityExceptionByLocationID(int locationID)
        {
            List<Availability> availabilities = new List<Availability>();

            try
            {
                foreach (LocationAvailabilityTableFake fake in _dbFake)
                {
                    if (fake.IsException)
                    {
                        foreach (Availability a in fake.Availabilities)
                        {
                            if (a.ForeignID == locationID)
                            {
                                availabilities.Add(a);
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return availabilities;
		}
		
        /// Logan Baccam
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// test that returns fake location tags for a location by location
        /// </summary>
        public List<string> SelectTagsbyLocationID(int locationID)
        {
            List<string> tags = new List<string>();
            if (_tags.ContainsKey(locationID))
            {
                tags = _tags[locationID];
            }
            return tags;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/22
        /// 
        /// Description:
        /// Inserts a review into the list of fake reviews
        /// 
        /// </summary>
        /// <param name="review"></param>
        /// <returns>rowsAffected</returns>
        public int InsertLocationReview(Reviews review)
        {
            int rowsAffected = 0;
            foreach (var s in _fakeLocations)
            {
                if (s.LocationID == review.ForeignID)
                {
                    _fakeLocationReviews.Add(review);
                    rowsAffected = 1;
                    break;
                }
            }
            if (rowsAffected == 0)
            {
                throw new ApplicationException("Invalid Location ID");
            }
            return rowsAffected;
        }
    }
}
