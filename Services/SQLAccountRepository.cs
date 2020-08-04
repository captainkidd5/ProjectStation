using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class SQLAccountRepository : IAccountRepository
    {
        private readonly AppDbContext context;

        public SQLAccountRepository(AppDbContext context)
        {
            this.context = context;
        }

        public UserAccount AddUserAccount(UserAccount newUserAccount)
        {
            throw new NotImplementedException();
        }

        public UserAccount Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserAccount GetUserAccount(int id)
        {
            throw new NotImplementedException();
        }

        public UserAccount Update(UserAccount updatedUserAccount)
        {
            throw new NotImplementedException();
        }
    }
}
