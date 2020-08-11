using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Models.Models
{
    public enum OrderStatus
    {
        Shopping = 0,
        AddressEntered = 1,
        PaidFor = 2,
        Completed = 10
    }
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
