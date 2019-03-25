using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IUserRepository
    {
        void AddUser(User user);

        void DeleteUser(int id);

        List<User> GetAllUsers();

        User GetUserById(int id);

        void UpdateUser(int id, User user);
    }
}
