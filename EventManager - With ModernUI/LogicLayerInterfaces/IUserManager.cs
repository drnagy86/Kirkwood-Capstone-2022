using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        // Should return a user object with roles
        // or throw an exception.
        User LoginUser(string email, string password);
        bool AuthenticateUserByEmailAndPassword(string email, string password);

        string HashSha256(string source);

        User RetrieveUserByEmail(string email);
        User RetrieveUserByUserID(int userID);

        List<String> RetrieveUserRolesByUserID(int UserID);

        bool UpdatePasswordHash(string email, string oldPassword, string newPassword);

        bool CreateUser(User user);
        bool CreateUserWithPassword(User user, string password);
        List<string> RetrieveAllRoles();
        bool RetrieveHasUserByEmail(string email);

        bool AddUserRole(int userID , string role);
        bool RemoveUserRole(int userID, string role);
    }
}
