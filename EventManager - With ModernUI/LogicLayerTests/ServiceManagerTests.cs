using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class ServiceManagerTests
    {
        private IServiceManager _serviceManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceManager = new ServiceManager(new ServiceAccessorFake());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Test that makes sure RetrieveServicesBySupplierID 
        /// returns a list with the correct number of services
        /// </summary>
        [TestMethod]
        public void TestRetrieveServicesBySupplierIDReturnsAmountOfServices()
        {
            // arrange
            const int supplierID = 100000;
            const int expected = 2;
            int actual;

            // act
            actual = _serviceManager.RetrieveServicesBySupplierID(supplierID).Count;

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Test that makes sure RetrieveServicesBySupplierID with a bad supplierID
        /// returns a list with no services 
        /// </summary>
        [TestMethod]
        public void TestRetrieveServicesBySupplierIDByBadSupplierIDReturnsAnEmptyList()
        {
            // arrange
            const int supplierID = 999999;
            const int expected = 0;
            int actual;

            // act
            actual = _serviceManager.RetrieveServicesBySupplierID(supplierID).Count;

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test that EditService returns true
        /// </summary>
        [TestMethod]
        public void TestEditServiceReturnsTrue()
        {
            // arrange
            Service oldService = new Service()
            {
                ServiceID = 100000,
                SupplierID = 100000,
                ServiceName = "Fake Service One",
                Price = 10.10m,
                Description = "The number one fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            };
            Service newService = new Service()
            {
                ServiceID = 100000,
                SupplierID = 100000,
                ServiceName = "Fake Service Ones",
                Price = 10.10m,
                Description = "The number ones fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            };

            const bool expected = true;
            bool actual;
            // act
            actual = _serviceManager.EditService(oldService, newService);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test that EditService returns false
        /// </summary>
        [TestMethod]
        public void TestEditServiceReturnsFalse()
        {
            // arrange
            Service oldService = new Service()
            {
                ServiceID = 1,
                SupplierID = 100000,
                ServiceName = "Fake Service One",
                Price = 10.10m,
                Description = "The number one fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            };
            Service newService = new Service()
            {
                ServiceID = 100000,
                SupplierID = 100000,
                ServiceName = "Fake Service Ones",
                Price = 10.10m,
                Description = "The number ones fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            };

            const bool expected = false;
            bool actual;
            // act
            actual = _serviceManager.EditService(oldService, newService);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test that CreateService returns true
        /// </summary>
        [TestMethod]
        public void TestCreateServiceReturnsTrue()
        {
            // arrange
            Service newService = new Service()
            {
                ServiceID = 1000070,
                SupplierID = 100000,
                ServiceName = "Fake Service Ones",
                Price = 10.10m,
                Description = "The number ones fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            };

            const bool expected = true;
            bool actual;
            // act
            actual = _serviceManager.CreateService(newService);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test that CreateService returns false
        /// </summary>
        [TestMethod]
        public void TestCreateServiceeReturnsFalse()
        {
            
            Service newService = new Service()
            {
                ServiceID = 100000,
                SupplierID = 100000,
                ServiceName = "Fake Service Ones",
                Price = 10.10m,
                Description = "The number ones fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            };

            const bool expected = false;
            bool actual;
            // act
            actual = _serviceManager.CreateService(newService);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test that CreateService returns true
        /// </summary>
        [TestMethod]
        public void TestRetrieveServiceByServiceIDReturnsCorrectService()
        {
            // arrange
            const int serviceID = 100000;
            Service expected = new Service()
            {
                ServiceID = 100000,
                SupplierID = 100000,
                ServiceName = "Fake Service One",
                Price = 10.10m,
                Description = "The number one fakest service out there",
                ServiceImagePath = "f43faecc-5d0f-4b4a-ba47-4c1d3ce56912.jpg"
            };

            Service actual;
            // act
            actual = _serviceManager.RetrieveServiceByServiceID(serviceID);

            // assert
            Assert.AreEqual(expected.ServiceID, actual.ServiceID);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.ServiceImagePath, actual.ServiceImagePath);
            Assert.AreEqual(expected.ServiceName, actual.ServiceName);
            Assert.AreEqual(expected.SupplierID, actual.SupplierID);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test that CreateService returns false
        /// </summary>
        [TestMethod]
        public void TestRetrieveServiceByServiceIDReturnsNullForBadID()
        {

            const int serviceID = 0;
            Service expected = null;
            Service actual;
            // act
            actual = _serviceManager.RetrieveServiceByServiceID(serviceID);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/28
        /// 
        /// Description:
        /// Test that DeleteService returns true
        /// </summary>
        [TestMethod]
        public void TestDeleteerviceReturnsTrue()
        {
            // arrange
            const int serviceID = 100000;

            const bool expected = true;
            bool actual;
            // act
            actual = _serviceManager.DeleteService(serviceID);

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/04/29
        /// 
        /// Description:
        /// Test that DeleteService returns false
        /// </summary>
        [TestMethod]
        public void TestDeleteServiceReturnsFalse()
        {


            const int serviceID = 1000070;
            const bool expected = false;
            bool actual;
            // act
            actual = _serviceManager.DeleteService(serviceID);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
