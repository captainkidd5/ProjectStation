using ProjectStation.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectStation.Services
{
    public class MockClientRepository : IClientRepository
    {

        private List<Client> clientList;


        public MockClientRepository()
        {
            clientList = new List<Client>()
            {
                new Client() {Id =0, Name = "Michael", Email = "300Cows@gmail.com"},
                new Client() {Id =1, Name = "Tucker", Email = "armadillogod@gmail.com"},
            };
        }

        public IEnumerable<Client> GetAllClients()
        {
            return clientList;
        }
    }
}
