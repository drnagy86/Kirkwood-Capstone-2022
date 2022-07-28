using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace DataAccessInterfaces
{
    public interface IUserAccessor
    {
        int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash);
        User SelectUserByEmail(string email);
        User SelectUserByUserID(int userID);
        List<string> SelectRolesByUserID(int userID);

        int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash);
        int InsertUser(User newUser);
        List<String> SelectAllRoles();
        int InsertUserRole(int userID, string role);
        int DeleteUserRole(int userID, string role);
    }
}
