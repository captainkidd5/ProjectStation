using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class MockClientRepository : IClientRepository
    {
        private List<Client> userList;
        public MockClientRepository()
        {
            userList = new List<Client>()
            {
                new Client() { Id=1, Name="Michael",Email="300Cows@gmail.com",
                ClientType=ClientType.Admin, PhotoPath="male.png"},
                new Client() { Id=2, Name="Kosuke",Email="kShimamura@gmail.com",
                ClientType=ClientType.Admin, PhotoPath="male.png"},
                new Client() { Id=3, Name="Tuck",Email="armadillogod@gmail.com",
                ClientType=ClientType.Admin, PhotoPath="male.png"},
            };
            
        }
        public IEnumerable<Client> GetAllClients()
        {
            return userList;
        }

        public Client GetClient(int id)
        {
            return userList.FirstOrDefault(x => x.Id == id);
        }

        public Client Update(Client updatedUser)
        {
            Client user = userList.FirstOrDefault(x => x.Id == updatedUser.Id);
            if(user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.ClientType = updatedUser.ClientType;
            }

            return user;
        }
    }
}
