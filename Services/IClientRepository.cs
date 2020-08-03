﻿using Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Services
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client GetClient(int id);
        Client Update(Client updatedClient);
    }
}