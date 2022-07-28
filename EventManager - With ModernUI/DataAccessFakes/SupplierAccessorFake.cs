using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataObjects;

namespace DataAccessFakes
{
    public class SupplierAccessorFake : ISupplierAccessor
    {
        private List<Supplier> _fakeSuppliers = new List<Supplier>();
        private List<Reviews> _fakeReviews = new List<Reviews>();
        private List<List<string>> _fakeImages = new List<List<string>>();
        private List<SupplierAvailabilityTableFake> _dbFake = new List<SupplierAvailabilityTableFake>();

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Constructor to populate _fakeSuppliers with dummy values for testing purposes
        /// 
        /// Christopher Repko
        /// Updated: 2022/02/11
        /// 
        /// Description: 
        /// Added fake reviews and image paths
        /// 
        /// Kris Howell
        /// Updated: 2022/02/18
        /// 
        /// Description:
        /// Add City, State, and Zip to supplier fakes
        /// </summary>
        public SupplierAccessorFake()
        {
            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100000,
                UserID = 100000,
                Name = "Test Supplier 1",
                Description = "Description of Test Supplier 1 goes here.",
                Phone = "111-111-1111",
                Email = "testSupplier1@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 1 Street",
                Address2 = "Apt 1",
                City = "Cedar Rapids",
                State = "Iowa",
                ZipCode = "52404",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true,
                Approved = null

            });

            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100001,
                UserID = 100000,
                Name = "Test Supplier 2",
                Description = "Description of Test Supplier 2 goes here.",
                Phone = "222-222-2222",
                Email = "testSupplier2@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 2 Street",
                Address2 = "Apt 2",
                City = "Iowa City",
                State = "Iowa",
                ZipCode = "52240",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true,
                Approved = true
            });

            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100002,
                UserID = 100001,
                Name = "Test Supplier 3",
                Description = "Description of Test Supplier 3 goes here.",
                Phone = "333-333-3333",
                Email = "testSupplier3@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 3 Street",
                Address2 = "Apt 3",
                City = "Cedar Rapids",
                State = "Iowa",
                ZipCode = "52404",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true,
                Approved = true
            });

            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100003,
                UserID = 100001,
                Name = "Test Supplier 4",
                Description = "Description of Test Supplier 4 goes here.",
                Phone = "444-444-4444",
                Email = "testSupplier4@suppliers.com",
                TypeID = "Vendor",
                Address1 = "Test Supplier 4 Street",
                Address2 = "Apt 4",
                City = "Iowa City",
                State = "Iowa",
                ZipCode = "52240",
                Tags = new List<string>()
                {
                    "Test Tag 1",
                    "Test Tag 2"
                },
                Active = true,
                Approved = null
            });

            _fakeSuppliers.Add(new Supplier()
            {
                SupplierID = 100004,
                UserID = 100000,
                Name = "Test Supplier 5",
                Description = "Description of Test Supplier 5 goes here.",
                Phone = "555-444-4444",
                Email = "testSupplier5@suppliers.com",
                Address1 = "Test Supplier 6 Street",
                Address2 = "Apt 7",
                City = "Iowa City",
                State = "Iowa",
                ZipCode = "52240"
            });

            _fakeReviews.Add(new Reviews()
            {
                ForeignID = 100000,
                ReviewID = 100000,
                FullName = "Whodunnit",
                ReviewType = "Supplier Review",
                Rating = 3,
                Review = "Could be better.",
                DateCreated = DateTime.Now,
                Active = true
            });

            _fakeReviews.Add(new Reviews()
            {
                ForeignID = 100000,
                ReviewID = 100020,
                FullName = "Whodunnit2",
                ReviewType = "Supplier Review2",
                Rating = 5,
                Review = "Amazing!",
                DateCreated = DateTime.Now,
                Active = true
            });

            _fakeImages.Add(new List<string>()
            {
                "Fakepath.png",
                "Fakepath2.png",
                "Fakepath3.png"
            });
            _fakeImages.Add(new List<string>()
            {
                "Fakepath.png"
            });

            _dbFake.Add(new SupplierAvailabilityTableFake()
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

            _dbFake.Add(new SupplierAvailabilityTableFake()
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

            _dbFake.Add(new SupplierAvailabilityTableFake()
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

            _dbFake.Add(new SupplierAvailabilityTableFake()
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

            _dbFake.Add(new SupplierAvailabilityTableFake()
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
        }

        private class SupplierAvailabilityTableFake
        {
            public DateTime Date { get; set; }
            public List<Availability> Availabilities { get; set; }
            public bool IsException { get; set; }
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Returns all active suppliers in fake supplier list
        /// </summary>
        /// 
        /// <returns>List of active suppliers</returns>
        public List<Supplier> SelectActiveSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            foreach (Supplier fakeSupplier in _fakeSuppliers)
            {
                if (fakeSupplier.Active)
                {
                    suppliers.Add(fakeSupplier);
                }
            }

            return suppliers;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Returns images associated with the selected supplier
        /// </summary>
        /// <param name="supplierID"> ID of supplier to retrieve images for</param>
        /// <returns>a list of strings representing image paths</returns>
        public List<string> SelectSupplierImagesBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();
            foreach (Supplier supplier in _fakeSuppliers)
            {
                if (supplier.SupplierID == supplierID)
                {
                    if (_fakeImages.Count > _fakeSuppliers.IndexOf(supplier))
                    {
                        result = _fakeImages[_fakeSuppliers.IndexOf(supplier)];
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Returns reviews associated with the selected supplier
        /// </summary>
        /// <param name="supplierID"> ID of supplier to retrieve reviews for</param>
        /// <returns>A list of reviews</returns>
        public List<Reviews> SelectSupplierReviewsBySupplierID(int supplierID)
        {
            List<Reviews> result = new List<Reviews>();
            foreach (Reviews review in _fakeReviews)
            {
                if (review.ForeignID == supplierID)
                {
                    result.Add(review);
                }
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Returns tags associated with the selected supplier
        /// </summary>
        /// <param name="supplierID"> ID of supplier to retrieve tags for</param>
        /// <returns>A list of strings</returns>
        public List<string> SelectSupplierTagsBySupplierID(int supplierID)
        {
            List<string> result = new List<string>();
            foreach (Supplier supplier in _fakeSuppliers)
            {
                if (supplier.SupplierID == supplierID)
                {
                    result = supplier.Tags;
                }
            }
            return result;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given supplierID and date.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Supplier on a given date</returns>
        public List<Availability> SelectSupplierAvailabilityBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Availability> availabilities = new List<Availability>();

            foreach (SupplierAvailabilityTableFake fake in _dbFake)
            {
                if (fake.Date == date && !fake.IsException)
                {
                    foreach (Availability a in fake.Availabilities)
                    {
                        if (a.ForeignID == supplierID)
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
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given supplierID and date.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="date"></param>
        /// <returns>A list of availability objects for a Supplier on a given date</returns>
        public List<Availability> SelectSupplierAvailabilityExceptionBySupplierIDAndDate(int supplierID, DateTime date)
        {
            List<Availability> availabilities = new List<Availability>();

            foreach (SupplierAvailabilityTableFake fake in _dbFake)
            {
                if (fake.Date == date && fake.IsException)
                {
                    foreach (Availability a in fake.Availabilities)
                    {
                        if (a.ForeignID == supplierID)
                        {
                            availabilities.Add(a);
                        }
                    }
                }
            }

            return availabilities;
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// Fake supplier availability for three months
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>List of Date times supplier is available</returns>
        public List<DateTime> SelectSupplierAvailabilityForNextThreeMonths(int supplierID)
        {
            List<DateTime> fakeDates = new List<DateTime>();

            foreach (SupplierAvailabilityTableFake avails in _dbFake)
            {
                foreach (Availability avail in avails.Availabilities)
                {
                    if (avail.ForeignID == supplierID && avail.TimeStart != null)
                    {
                        fakeDates.Add((DateTime)avail.TimeStart);
                    }
                }
            }

            return fakeDates;
        }

        /// Austin Timmerman
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Select regular weekly availability records matching the given supplierID;
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of availability objects for a Supplier</returns>
        public List<AvailabilityVM> SelectSupplierAvailabilityBySupplierID(int supplierID)
        {
            List<AvailabilityVM> availabilities = new List<AvailabilityVM>();

            try
            {
                foreach (SupplierAvailabilityTableFake fake in _dbFake)
                {
                    if (!fake.IsException)
                    {
                        foreach (Availability a in fake.Availabilities)
                        {
                            if (a.ForeignID == supplierID)
                            {
                                switch (fake.Date.DayOfWeek)
                                {
                                    case DayOfWeek.Monday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = supplierID,
                                            Monday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Tuesday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = supplierID,
                                            Tuesday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Wednesday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = supplierID,
                                            Wednesday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Thursday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = supplierID,
                                            Thursday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Friday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = supplierID,
                                            Monday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Saturday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = supplierID,
                                            Monday = true,
                                            TimeStart = a.TimeStart,
                                            TimeEnd = a.TimeEnd
                                        });
                                        break;
                                    case DayOfWeek.Sunday:
                                        availabilities.Add(new AvailabilityVM
                                        {
                                            ForeignID = supplierID,
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
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given supplierID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A list of availability objects for a Supplier</returns>
        public List<Availability> SelectSupplierAvailabilityExceptionBySupplierID(int supplierID)
        {
            List<Availability> availabilities = new List<Availability>();

            try
            {
                foreach (SupplierAvailabilityTableFake fake in _dbFake)
                {
                    if (fake.IsException)
                    {
                        foreach (Availability a in fake.Availabilities)
                        {
                            if (a.ForeignID == supplierID)
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

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Retrieves a supplier from the fake supplier list.
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns>A supplier with the given supplierId</returns>
        public Supplier SelectSupplierBySupplierID(int supplierID)
        {
            if (supplierID < 99999)
            {
                throw new ApplicationException("Supplier not found.");
            }
            Supplier _supplier = new Supplier();
            foreach (Supplier supplier in _fakeSuppliers)
            {
                if (supplier.SupplierID == supplierID)
                {
                    _supplier = supplier;
                }
            }
            if (_supplier is null || _supplier.Name.Length == 0)
            {
                throw new ApplicationException("Supplier not found.");
            }

            return _supplier;
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
        public int InsertSupplierReview(Reviews review)
        {
            int rowsAffected = 0;
            foreach (var s in _fakeSuppliers)
            {
                if (s.SupplierID == review.ForeignID)
                {
                    _fakeReviews.Add(review);
                    rowsAffected = 1;
                    break;
                }
            }
            if (rowsAffected == 0)
            {
                throw new ApplicationException("Invalid Supplier ID");
            }
            return rowsAffected;
        }

        /// Christopher Repko
        /// Created: 2022/04/26
        /// 
        /// Description:
        /// Retrieves all unapproved suppliers from the fakes.
        /// </summary>
        /// <returns>List of all unapproved suppliers</returns>
        public List<Supplier> SelectUnapprovedSuppliers()
        {
            List<Supplier> result = new List<Supplier>();
            foreach (Supplier supplier in _fakeSuppliers)
            {
                if (supplier.Approved == null)
                {
                    result.Add(supplier);
                }
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Changes a supplier's approval status to "approved"
        /// </summary>
        /// <param name="supplierID">ID of supplier</param>
        /// <returns>the number of records affected</returns>
        public int ApproveSupplier(int supplierID)
        {
            int result = 0;
            foreach (Supplier supplier in _fakeSuppliers)
            {
                if (supplier.SupplierID == supplierID)
                {
                    supplier.Approved = true;
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Changes a supplier's approval status to "disapproved"
        /// </summary>
        /// <param name="supplierID">ID of supplier</param>
        /// <returns>the number of records affected</returns>
        public int DisapproveSupplier(int supplierID)
        {
            int result = 0;
            foreach (Supplier supplier in _fakeSuppliers)
            {
                if (supplier.SupplierID == supplierID)
                {
                    supplier.Approved = false;
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/27
        /// 
        /// Description:
        /// Marks a supplier as needing to be reviewed again.
        /// </summary>
        /// <param name="supplierID">ID of supplier</param>
        /// <returns>the number of records affected</returns>
        public int RequeueSupplier(int supplierID)
        {
            int result = 0;
            foreach (Supplier supplier in _fakeSuppliers)
            {
                if (supplier.SupplierID == supplierID)
                {
                    supplier.Approved = null;
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Inserts a fake supplier with no userID for supplier request
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>row affected</returns>
        public int InsertSupplier(Supplier supplier)
        {
            int rows = 0;
            _fakeSuppliers.Add(supplier);
            rows += 1;

            return rows;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Grabs a list of all suppliers with a given userID.
        /// </summary>
        /// <param name="userID">UserID to be searched for.</param>
        /// <returns>a list of all suppliers with a given userID</returns>
        public List<Supplier> SelectSuppliersByUserID(int userID)
        {
            List<Supplier> result = new List<Supplier>();
            foreach(Supplier supplier in _fakeSuppliers)
            {
                if(supplier.UserID == userID)
                {
                    result.Add(supplier);
                }
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Method to replace one supplier with another in the data.
        /// </summary>
        /// <param name="oldSupplier">Supplier to replace</param>
        /// <param name="newSupplier">Supplier to replace with</param>
        /// <returns>rows affected</returns>
        public int UpdateSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
            int result = 0;
            foreach(Supplier supplier in _fakeSuppliers)
            {
                if (supplier.SupplierID == oldSupplier.SupplierID)
                {
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Insert availability object into data persistence
        /// </summary>
        /// <param name="availability">availability object to insert</param>
        /// <returns></returns>
        public int InsertSupplierAvailability(Availability availability)
        {
            int rowsAffected = 0;

            // no supplier ID
            if (availability.ForeignID == 0)
            {
                throw new ApplicationException("Missing Supplier ID.  Please try again.");
            }

            // no time start
            if (availability.TimeStart == null || availability.TimeEnd == new DateTime())
            {
                throw new ApplicationException("Please select a starting time.");
            }
            DateTime timeStart = (DateTime)availability.TimeStart;

            // no end time
            if (availability.TimeEnd == null || availability.TimeEnd == new DateTime())
            {
                throw new ApplicationException("Please select an ending time.");
            }
            DateTime timeEnd = (DateTime)availability.TimeEnd;

            // no days selected
            if (!availability.Sunday && !availability.Monday && !availability.Tuesday && !availability.Wednesday &&
                !availability.Thursday && !availability.Friday && !availability.Saturday)
            {
                throw new ApplicationException("Please select at least one day of the week.");
            }

            // start time after end time
            if (timeStart.CompareTo(timeEnd) >= 0)
            {
                throw new ApplicationException("Your starting time must be before your ending time");
            }

            SupplierAvailabilityTableFake toInsert = new SupplierAvailabilityTableFake
            {
                Date = availability.DateID.Value,
                Availabilities = new List<Availability>(),
                IsException = false
            };
            toInsert.Availabilities.Add(availability);

            _dbFake.Add(toInsert);
            rowsAffected++;

            return rowsAffected;
        }
    }
}
