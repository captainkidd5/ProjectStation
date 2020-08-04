using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Services
{
    public class MockClientRepository : IClientRepository
    {
        private List<Client> clientList;
        public MockClientRepository()
        {
            clientList = new List<Client>()
            {
                new Client() { Id=1, Name="Michael",Email="300Cows@gmail.com",
                ClientType=ClientType.Admin, PhotoPath="anon.png"},
                new Client() { Id=2, Name="Kosuke",Email="kShimamura@gmail.com",
                ClientType=ClientType.Admin, PhotoPath="anon.png"},
                new Client() { Id=3, Name="Tuck",Email="armadillogod@gmail.com",
                ClientType=ClientType.Admin, PhotoPath="anon.png"},
            };
            
        }

        public Client AddClient(Client newClient)
        {
            newClient.Id = clientList.Max(x => x.Id) + 1;
            clientList.Add(newClient);
            return newClient;
        }

        public IEnumerable<ClientTypHeadCount> ClientCountByType(ClientType? clientType)
        {
            IEnumerable<Client> query = clientList;
            if(clientType.HasValue)
            {
                query = query.Where(x => x.ClientType == clientType.Value);
            }
            return query.GroupBy(x => x.ClientType)
                .Select(g => new ClientTypHeadCount()
                {
                    ClientType = g.Key.Value,
                    Count = g.Count(),
                }).ToList();
        }

        public Client Delete(int id)
        {
            Client clientToDelete = clientList.FirstOrDefault(x => x.Id == id);
            if(clientToDelete != null)
            {
                clientList.Remove(clientToDelete);
            }
            return clientToDelete;
        }

        public IEnumerable<Client> GetAllClients()
        {
            return clientList;
        }

        public Client GetClient(int id)
        {
            return clientList.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Client> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return clientList;
            }

            return clientList.Where(x => x.Name.Contains(searchTerm) || x.Email.Contains(searchTerm));
        }

        public Client Update(Client updatedUser)
        {
            Client user = clientList.FirstOrDefault(x => x.Id == updatedUser.Id);
            if(user != null)
            {
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.ClientType = updatedUser.ClientType;
                user.PhotoPath = updatedUser.PhotoPath;
            }

            return user;
        }
    }
}
