using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersWebAPI.Models
{
    public class OrdersModel
    {
        public int OrderID { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public int ShippingCost { get; set; }
    }
}