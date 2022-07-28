using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using DataAccessLayer;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        // Dependency is on IUserAccessor provider. This takes a tightly-coupled setup and makes it loosely coupled.
        private IUserAccessor _userAccessor;

        // A default constructor for most users
        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        // Overloaded accessor allows calling code to
        // supply its own IUserAccessor provider.s
        public UserManager(IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;
        }

        /// <summary>
        /// 
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Method to authenticate the user's credentials
        /// </summary>
        /// <param name="email">Email to be used as credentials</param>
        /// <param name="password">Password to be used as credentials</param>
        /// <returns>true if the user is authenticated, otherwise false.</returns>
        public bool AuthenticateUserByEmailAndPassword(string email, string password)
        {
            bool result = false;
            try
            {
                result = 1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Method to get a SHA265 hash from a string 
        /// </summary>
        /// <param name="source">String to be hashed</param>
        /// <returns>Hexidecimal string representing hash data</returns>
        public string HashSha256(string source)
        {
            string result = "";

            // create a byte array
            Byte[] data;

            //Create hash provider object
            using (SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            // create an output stringbuilder object.
            var s = new StringBuilder();

            // loop through the hashed output making characters
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString().ToUpper();

            return result;
        }

        /// <summary>
        /// 
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Method to retrieve user data using an email address.
        /// 
        /// Christopher Repko
        /// Updated: 2022/03/24
        /// 
        /// Description:
        /// Added roles to the data added when retrieving a user.
        /// </summary>
        /// <param name="email">Email to be used to retrieve user</param>
        /// <returns>User with matching email.</returns>
        public User RetrieveUserByEmail(string email)
        {
            User requestedUser = null;

            try
            {
                requestedUser = this._userAccessor.SelectUserByEmail(email);
                requestedUser.Roles = this._userAccessor.SelectRolesByUserID(requestedUser.UserID);
            }
            catch (Exception)
            {
                throw;
            }

            return requestedUser;
        }

        public List<string> RetrieveUserRolesByUserID(int userID)
        {
            List<String> roles = null;
            try
            {
                roles = this._userAccessor.SelectRolesByUserID(userID);
            }
            catch (Exception)
            {

                throw;
            }
            return roles;
        }

        /// <summary>
        /// 
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Method to authenticate and retrieve a user
        /// </summary>
        /// <param name="email">Email to be used as credentials</param>
        /// <param name="password">Password to be used as credentials</param>
        /// <exception cref="ApplicationException">Thrown if the user cannot be found or if the user does not enter an email or password.</exception>
        /// <returns>An object representing the user logged in. </returns>
        public User LoginUser(string email, string password)
        {
            User loggedInUser = null;
            try
            {
                if (email == "")
                {
                    throw new ArgumentException("Missing email.");
                }
                if (password == "") // or fails complexity rules.
                {
                    throw new ArgumentException("Missing password.");
                }

                password = this.HashSha256(password);
                if (this.AuthenticateUserByEmailAndPassword(email, password))
                {
                    loggedInUser = this.RetrieveUserByEmail(email);
                    //loggedInUser.Roles = this.GetRolesForUser(loggedInUser.UserID);
                }
                else
                {
                    throw new ApplicationException("Bad Email Address or Password.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Login failed. Please check your credentials.", ex);
            }

            return loggedInUser;
        }

        /// <summary>
        /// 
        /// Christopher Repko
        /// Created: 2022/01/21
        /// 
        /// Description:
        /// Method to reset a user's password
        /// 
        /// </summary>
        /// <param name="email">Email of user requesting password reset</param>
        /// <param name="oldPassword">Old password to be changed</param>
        /// <param name="newPassword">New password to change to</param>
        /// <exception cref="ApplicationException">Thrown if something causes password reset to fail.</exception>
        /// <returns>true if password reset works, otherwise false</returns>
        public bool UpdatePasswordHash(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            try
            {
                string oldPasswordHash = this.HashSha256(oldPassword);
                string newPasswordHash = this.HashSha256(newPassword);

                result = (1 == this._userAccessor.UpdatePasswordHash(email, oldPasswordHash, newPasswordHash));
                if (!result)
                {
                    throw new ApplicationException("Could not update password.");
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to update password", e);
            }


            return result;
        }

        /// <summary>
        /// Ramiro Pena
        /// Created: Unknown
        /// 
        /// Method to create user entry
        /// </summary>
        /// <param name="user">User object containing data of the user to create</param>
        /// <returns>true if update goes through, false otherwise.</returns>
        public bool CreateUser(User user)
        {
            bool result = false;
            try
            {
                result = 1 == _userAccessor.InsertUser(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/3/25
        /// 
        /// Description:
        /// Method to retrieve a list of all roles in the database 
        /// 
        /// </summary>
        /// <returns>List of strings with all roles in database</returns>
        public List<string> RetrieveAllRoles()
        {
            List<string> result = new List<String>();
            try
            {
                result = this._userAccessor.SelectAllRoles();
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to retrieve user roles", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/3/25
        /// 
        /// Description:
        /// Method to check if a user exists with a specific email address
        /// 
        /// </summary>
        /// <param name="email">Email address to check for</param>
        /// <returns>true if the user exists in the database, otherwise false</returns>
        public bool RetrieveHasUserByEmail(string email)
        {
            try
            {
                return _userAccessor.SelectUserByEmail(email) != null;
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "User not found.")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Database error", ex);
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/3/25
        /// 
        /// Description:
        /// Method to create a user and update their password.
        /// 
        /// </summary>
        /// <param name="user">User data to create a database entry</param>
        /// <param name="password">Password to set the user's password to</param>
        /// <returns>true if successful, otherwise false</returns>
        public bool CreateUserWithPassword(User user, string password)
        {
            bool result = false;
            try
            {
                result = CreateUser(user);
                result = result && UpdatePasswordHash(user.EmailAddress, "P@ssw0rd", password);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to create user", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/3/25
        /// 
        /// Description:
        /// Method to add a role to a user
        /// 
        /// </summary>
        /// <param name="userID">ID of user to add role to</param>
        /// <param name="role">role to add</param>
        /// <returns>True if successful, otherwise false</returns>
        public bool AddUserRole(int userID, string role)
        {
            bool result = false;
            try
            {
                result = 1 == this._userAccessor.InsertUserRole(userID, role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to add user role", ex);
            }
            return result;
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/3/25
        /// 
        /// Description:
        /// Method to remove a role from a user
        /// 
        /// </summary>
        /// <param name="userID">User ID to remove role for</param>
        /// <param name="role">role to remove</param>
        /// <returns>true if successful, otherwise false</returns>
        public bool RemoveUserRole(int userID, string role)
        {
            bool result = false;
            try
            {
                result = 1 == this._userAccessor.DeleteUserRole(userID, role);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to remove role", ex);
            }
            return result;
        }

        public User RetrieveUserByUserID(int userID)
        {
            User result = null;

            try
            {
                result = _userAccessor.SelectUserByUserID(userID);
            } catch(Exception ex)
            {
                throw new ApplicationException("Failed to retrieve user information.", ex);
            }

            return result;
        }
    }
}
