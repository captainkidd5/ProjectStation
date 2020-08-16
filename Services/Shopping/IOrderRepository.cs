using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shopping
{
    public interface IOrderRepository
    {
        bool Add(Order order);
        bool ChangeStatus(Order order, OrderStatus status);
    }
}
