using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlinePurchaseWebAPI.Models
{
    public class CustomerInfo
    {
        public string CustomerName { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
    }

    public class OrderInfo
    {
        public string OrderStatus { get; set; }
        public string Order_Date { get; set; }
    }

    public class ProductInfo
    {
        public string ProductName { get; set; }
        public int OrderQuantity { get; set; }
        public string ItemPrice { get; set; }
        public string TotalPrice { get; set; }
    }

    public class OrderItemDetails
    {
        public CustomerInfo Customers { get; set; }
        public OrderInfo Orders { get; set; }
        public ProductInfo Products { get; set; }
    }

}