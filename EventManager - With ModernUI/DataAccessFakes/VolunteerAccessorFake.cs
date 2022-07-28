using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// The VolunteerAccessorFake for all volunteer fake data
    /// </summary>
    public class VolunteerAccessorFake : IVolunteerAccessor
    {
        List<Volunteer> _fakeVolunteers = new List<Volunteer>();
        private List<String> _fakePasswordHashes = new List<string>();
        private List<VolunteerAvailabilityTableFake> _dbFake = new List<VolunteerAvailabilityTableFake>();

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The volunteer accessor fakes for fake volunteers
        /// </summary>
        public VolunteerAccessorFake()
        {

            this._fakeVolunteers.Add(new Volunteer()
            {
                VolunteerID = 999999,
                UserID = 999999,
                GivenName = "Tess",
                FamilyName = "Data",
                Email = "tess@company.com",
                State = "IA",
                City = "Atkins",
                Zip = 52206,
                Active = true,
                Rating = 5,
                DateCreated = new DateTime(2022, 03, 05)
            });
            this._fakeVolunteers.Add(new Volunteer()
            {
                VolunteerID = 999998,
                UserID = 999998,
                GivenName = "Duplicate",
                FamilyName = "Data",
                Email = "duplicate@company.com",
                State = "IA",
                City = "Atkins",
                Zip = 52206,
                Active = true,
                Rating = 4,
                DateCreated = DateTime.Now
            });
            this._fakeVolunteers.Add(new Volunteer()
            {
                VolunteerID = 999997,
                UserID = 999997,
                GivenName = "Duplicate",
                FamilyName = "Data",
                Email = "duplicate@company.com",
                State = "IA",
                City = "Atkins",
                Zip = 52206,
                Active = true,
                Rating = 3,
                DateCreated = DateTime.Now
            });

            _dbFake.Add(new VolunteerAvailabilityTableFake()
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

            _dbFake.Add(new VolunteerAvailabilityTableFake()
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

            _dbFake.Add(new VolunteerAvailabilityTableFake()
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

            _dbFake.Add(new VolunteerAvailabilityTableFake()
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

            _dbFake.Add(new VolunteerAvailabilityTableFake()
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

            this._fakePasswordHashes.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this._fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this._fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// VolunteerAvailabilityTableFake
        /// </summary>
        private class VolunteerAvailabilityTableFake
        {
            public DateTime Date { get; set; }
            public List<Availability> Availabilities { get; set; }
            public bool IsException { get; set; }
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The accessor fake to select all volunteer reviews
        /// </summary>
        /// <returns>A list of volunteer data object shells for the volunteer ID and rating</returns>
        public List<Volunteer> SelectAllVolunteerReviews()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _fakeVolunteers;
            }
            catch (Exception)
            {

                throw;
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The accessor fake to select all volunteers
        /// </summary>
        /// <returns>A list of volunteer data objects</returns>
        public List<Volunteer> SelectAllVolunteers()
        {
            List<Volunteer> volunteers = new List<Volunteer>();

            try
            {
                volunteers = _fakeVolunteers;
            }
            catch (Exception)
            {

                throw;
            }

            return volunteers;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Select availability records matching the given volunteerID
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>A list of availability objects for a Volunteer</returns>
        /// (Original Author: Kris Howell LocationAccessor.cs)
        public List<Availability> SelectAvailabilityByVolunteerIDAndDate(int volunteerID, DateTime date)
        {
            List<Availability> availabilities = new List<Availability>();

            foreach (VolunteerAvailabilityTableFake fake in _dbFake)
            {
                if (fake.Date == date && !fake.IsException)
                {
                    foreach (Availability a in fake.Availabilities)
                    {
                        if (a.ForeignID == volunteerID)
                        {
                            availabilities.Add(a);
                        }
                    }
                }
            }

            return availabilities;
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Select one-off availability exception records matching the given volunteerID
        /// </summary>
        /// <param name="volunteerID"></param>
        /// <returns>A list of availability objects for a Volunteer</returns>
        /// (Original Author: Kris Howell LocationAccessor.cs)
        public List<Availability> SelectAvailabilityExceptionByVolunteerIDAndDate(int volunteerID, DateTime date)
        {
            List<Availability> availabilities = new List<Availability>();

            foreach (VolunteerAvailabilityTableFake fake in _dbFake)
            {
                if (fake.Date == date && fake.IsException)
                {
                    foreach (Availability a in fake.Availabilities)
                    {
                        if (a.ForeignID == volunteerID)
                        {
                            availabilities.Add(a);
                        }
                    }
                }
            }

            return availabilities;
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/04/07
        /// 
        /// Description:
        /// Selects a fake volunteer object with a specific userID
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>A Volunteer object</returns>
        public Volunteer SelectVolunteerByUserID(int userID)
        {
            Volunteer volunteer = null;
            foreach(Volunteer v in _fakeVolunteers)
            {
                if(v.UserID == userID)
                {
                    volunteer = v;
                    break;
                }
            }
            return volunteer;
        }
    }
}
