using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime DateTime { get; set; }
        public string Session { get; set; }
        public float Total { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
