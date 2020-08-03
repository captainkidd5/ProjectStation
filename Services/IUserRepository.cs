using Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Services
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
    }
}
