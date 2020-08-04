using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Services
{
    public class SQLClientRepository : IClientRepository
    {
        private readonly AppDbContext context;

        public SQLClientRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Client AddClient(Client newClient)
        {

            context.Database.ExecuteSqlRaw("spInsertClient {0},{1},{2},{3}",
                                            newClient.Name,
                                            newClient.Email,
                                            newClient.PhotoPath,
                                            newClient.ClientType);


            return newClient;
        }

        public IEnumerable<ClientTypHeadCount> ClientCountByType(ClientType? clientType)
        {
            IEnumerable<Client> query = context.Clients;
            if (clientType.HasValue)
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
            Client client = context.Clients.Find(id); 
            if(client != null)
            {
                context.Clients.Remove(client);
                context.SaveChanges();
            }
            return client;

        }

        public IEnumerable<Client> GetAllClients()
        {
            return context.Clients
                .FromSqlRaw<Client>("SELECT * FROM Clients")
                .ToList();
        }

        public Client GetClient(int id)
        {
            SqlParameter parameter = new SqlParameter("Id", id);

            return context.Clients.FromSqlRaw<Client>("spGetClientById @Id", parameter)
                .ToList()
                .FirstOrDefault();
        }

        public IEnumerable<Client> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Clients;
            }

            return context.Clients.Where(x => x.Name.Contains(searchTerm) || x.Email.Contains(searchTerm));
        }

        public Client Update(Client updatedClient)
        {
            var client = context.Clients.Attach(updatedClient);
            client.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedClient;
        }
    }
}
