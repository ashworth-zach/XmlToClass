using System;
using System.Collections.Generic;

namespace DynamicXmlCasting.Models{

    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string SupplierID { get; set; }
        public string CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public string UnitPrice { get; set; }
        public string UnitsInStock { get; set; }
        public string UnitsOnOrder { get; set; }
        public string ReorderLevel { get; set; }
        public string Discontinued { get; set; }
    }

    public class OrderDetail
    {
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public string UnitPrice { get; set; }
        public string Quantity { get; set; }
        public string Discount { get; set; }
        public Product Product { get; set; }
    }

    public class Order
    {
        public string OrderID { get; set; }
        public string CustomerID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime ShippedDate { get; set; }
        public string ShipVia { get; set; }
        public string Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public List<OrderDetail> Order_Details { get; set; }
    }

    public class Orders
    {
        public Order Order { get; set; }
    }

    public class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public Orders Orders { get; set; }
    }
}