using ProjectStation.Models;
using System;
using System.Collections.Generic;

namespace ProjectStation.Services
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
    }
}
