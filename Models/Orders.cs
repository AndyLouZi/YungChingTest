﻿using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Orders
    {
        [Key]
        public int OrderID { get;set;}
        public string? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }

        public float? Freight { get; set; }
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string? ShipPostalCode { get; set; }
        public string? ShipCountry { get; set; }

        // 導覽屬性：對應到單一的 Customer
        public Customers Customer { get; set; }

    }
}
