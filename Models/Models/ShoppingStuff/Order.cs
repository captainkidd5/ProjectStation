using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public enum OrderStatus
    {
        PaidFor = 1,

        Shipped = 2
    }
    public class Order
    {
        [Key]
        public string Id { get; set; }

        public string CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyName { get; set; }

        public string Country { get; set; }

        public string StreetNumber { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string OrderNotes { get; set; }

        public DateTime DateTime { get; set; }
        
        public OrderStatus OrderStatus { get; set; }

        public float Total { get; set; }

    }
}
