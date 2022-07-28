using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LogicLayer;
using DataAccessFakes;
using DataObjects;
using LogicLayerInterfaces;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        private IUserManager userManager;

        [TestInitialize]
        public void TestSetup()
        {

            userManager = new UserManager(new UserAccessorFake());
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests that SHA-256 hashing works correctly
        /// 
        /// </summary>
        [TestMethod]
        public void TestHashSha256ReturnsCorrectHashValue()
        {
            // Arrange
            const string source = "newuser";
            const string ExpectedResult = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            string actualResult = "";

            //Act
            actualResult = userManager.HashSha256(source);

            //Assert
            Assert.AreEqual(ExpectedResult, actualResult);
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests that authentication logic works
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernamePasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            string passwordHash = "b03ddf3ca2e714a6548e7495e2a03f5e824eaac9837cd7f159c67b90fb4b7342".ToUpper();
            const bool ExpectedResult = true;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests authentication logic rejects incorrect username
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectUsername()
        {
            // Arrange
            const string email = "tess-x@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests authentication logic rejects incorrect password
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectPasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            const string passwordHash = "x9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests authentication logic rejects cases where multiple users are returned from the same credentials
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserFailsWithDuplicateUsers()
        {
            // Arrange
            const string email = "duplicate@company.com";
            const string passwordHash = "dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests that user selection logic grabs correct user
        /// 
        /// </summary>
        [TestMethod]
        public void TestSelectUserByEmailReturnsCorrectUser()
        {
            // Arrange
            User user = null;
            const string expectedUserEmail = "tess@company.com";
            int expectedUserID = 999999;
            int actualUserID = 0;
            // Act
            user = userManager.RetrieveUserByEmail(expectedUserEmail);
            actualUserID = user.UserID;
            // Assert
            Assert.AreEqual(expectedUserID, actualUserID);
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests user selection logic throws an exception if there is no valid user.
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectUserByEmailReturnsApplicateExceptionForBadEmail()
        {
            // Arrange
            User user = null;
            const string badUserEmail = "xtess@company.com";
            // Act
            user = userManager.RetrieveUserByEmail(badUserEmail);
            // Assert
            // Nothing to do, checking for exception.
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests password reset logic works correctly
        /// 
        /// </summary>
        [TestMethod]
        public void TestResetPasswordWorksWithValidPasswords()
        {
            // Arrange
            const string oldPassword = "P@ssw0rd";
            const string newPassword = "newuser";
            const string email = "tess@company.com";
            const bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = userManager.UpdatePasswordHash(email, oldPassword, newPassword);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests password reset logic rejects passwords with incorrect old password.
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestResetPasswordThrowsExceptionWithBadOldPassword()
        {
            // Arrange
            const string oldPassword = "xnewuser";
            const string newPassword = "P@ssw0rd";
            const string email = "tess@company.com";

            // Act
            userManager.UpdatePasswordHash(email, oldPassword, newPassword);

            // Assert
            // Nothing to do here

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests password reset logic rejects bas email addresses
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestResetPasswordThrowsExceptionWithBadEmail()
        {
            // Arrange
            const string oldPassword = "newuser";
            const string newPassword = "P@ssw0rd";
            const string email = "xtess@company.com";

            // Act
            userManager.UpdatePasswordHash(email, oldPassword, newPassword);

            // Assert
            // Nothing to do here

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/2/4
        /// 
        /// Description:
        /// Tests password reset logic rejects bas email addresses
        /// 
        /// </summary>
        [TestMethod]
        public void TestCreateUserReturnsTrue()
        {
            // Arrange
            const bool expected = true;
            bool result;
            User user = new User()
            {
                UserID = 0,
                GivenName = "Testy",
                FamilyName = "McTest",
            };

            // Act
            result =  userManager.CreateUser(user);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Tests logic to get all roles
        /// 
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllRolesRetrievesAllRoles()
        {
            // Arrange
            List<string> expected = new List<string>();
            expected.Add("Administrator");
            expected.Add("Event Planner");

            List<string> result;
            // Act
            result = userManager.RetrieveAllRoles();

            // Assert
            // Nothing to do here
            CollectionAssert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Tests logic to check if database has a user
        /// 
        /// </summary>
        [TestMethod]
        public void TestRetrieveHasUserByEmailReturnsTrue()
        {
            // Arrange
            const bool expected = true;
            string email = "tess@company.com";
            bool result;

            // Act
            result = userManager.RetrieveHasUserByEmail(email);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Tests logic to check if database has a user
        /// 
        /// </summary>
        [TestMethod]
        public void TestRetrieveHasUserByEmailReturnsFalse()
        {
            // Arrange
            const bool expected = false;
            string email = "tesas@company.com";
            bool result;

            // Act
            result = userManager.RetrieveHasUserByEmail(email);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/03/24
        /// 
        /// Description:
        /// Tests Create user with password returns true
        /// 
        /// </summary>
        [TestMethod]
        public void TestCreateUserWithPasswordReturnsTrue()
        {
            // Arrange
            const bool expected = true;
            bool result;
            User user = new User()
            {
                UserID = 0,
                GivenName = "Testy",
                FamilyName = "McTest",
            };
            string password = "Password!";
            // Act
            result = userManager.CreateUserWithPassword(user, password);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Tests AddRole returns true for good user ID
        /// 
        /// </summary>
        [TestMethod]
        public void TestAddUserRoleReturnsTrue()
        {
            // Arrange
            const bool expected = true;
            const int UserID = 999999;
            bool result;
            string role = "Tester";
            // Act
            result = userManager.AddUserRole(UserID, role);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Tests AddRole returns false for bad user ID
        /// 
        /// </summary>
        [TestMethod]
        public void TestAddUserRoleReturnsFalseForBadUserID()
        {
            // Arrange
            const bool expected = false;
            const int UserID = 2342140;
            bool result;
            string role = "Tester";
            // Act
            result = userManager.AddUserRole(UserID, role);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Tests RemoveRole returns true for good user ID
        /// 
        /// </summary>
        [TestMethod]
        public void TestRemoveUserRoleReturnsTrue()
        {
            // Arrange
            const bool expected = true;
            const int UserID = 999999;
            bool result;
            string role = "Tester";
            // Act
            result = userManager.RemoveUserRole(UserID, role);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Tests RemoveRole returns false for bad user ID
        /// 
        /// </summary>
        [TestMethod]
        public void TestRemoveUserRoleReturnsFalseForBadUserID()
        {
            // Arrange
            const bool expected = false;
            const int UserID = 2342140;
            bool result;
            string role = "Tester";
            // Act
            result = userManager.RemoveUserRole(UserID, role);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Tests RemoveRole returns false for bad user ID
        /// 
        /// </summary>
        [TestMethod]
        public void TestRetrieveUserByUserIDRetrievesUser()
        {
            // Arrange
            const int UserID = 999997;
            User expected = new User()
            {
                UserID = 999997,
                EmailAddress = "duplicate@company.com",
                GivenName = "Tess",
                FamilyName = "Data",
                State = "IA",
                City = "Cedar Rapids",
                Zip = 12345,
                Roles = new List<String>(),
                Active = true
            };
            User result;
            // Act
            result = userManager.RetrieveUserByUserID(UserID);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected.FamilyName, result.FamilyName);
            Assert.AreEqual(expected.GivenName, result.GivenName);
            Assert.AreEqual(expected.State, result.State);
            Assert.AreEqual(expected.UserDescription, result.UserDescription);
            Assert.AreEqual(expected.UserID, result.UserID);
            Assert.AreEqual(expected.UserPhoto, result.UserPhoto);
            Assert.AreEqual(expected.Zip, result.Zip);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/03/25
        /// 
        /// Description:
        /// Tests RemoveRole returns false for bad user ID
        /// 
        /// </summary>
        [TestMethod]
        public void TestRetrieveUserByUserIDReturnsNullForBadID()
        {
            // Arrange
            User expected = null;
            const int userID = 0;
            User result;
            // Act
            result = userManager.RetrieveUserByUserID(userID);

            // Assert
            // Nothing to do here
            Assert.AreEqual(expected, result);
        }
    }
    
}
