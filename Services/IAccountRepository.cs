using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IAccountRepository
    {
        UserAccount GetUserAccount(int id);
        UserAccount Update(UserAccount updatedUserAccount);
        UserAccount AddUserAccount(UserAccount newUserAccount);
        UserAccount Delete(int id);
    }
}
