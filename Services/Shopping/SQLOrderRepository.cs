using Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Services.Shopping
{
    public class SQLOrderRepository : IOrderRepository
    {


        private readonly AppDbContext appDbContext;


        public SQLOrderRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public bool Add(Order order)
        {
            if(appDbContext.Orders.Find(order) != null)
            {
                appDbContext.Orders.Add(order);
                appDbContext.SaveChanges();
                return true;
            }
            return false;
            
        }

        public bool ChangeStatus(Order order, OrderStatus status)
        {
            order.OrderStatus = status;

            var Order = appDbContext.Orders.Attach(order);
            Order.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();
            return true;
        }
    }
}
