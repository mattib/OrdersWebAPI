using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using OrdersWebAPI.Models;
using OrdersDAL;

namespace OrdersWebAPI.Controllers
{
    public class OrdersController : ApiController
    {
        // GET: api/Orders
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<List<OrdersModel>> Get()
        {
            var task = Task.Run(() => UseMockData() ? GetMockOrders() : GetOrders());

            return await task;

        }

        private bool UseMockData()
        {
            return bool.Parse(System.Configuration.ConfigurationManager.AppSettings["MockData"]);
        }

        private List<OrdersModel> GetOrders()
        {
            using (var dataContext = new NorthwindEntities())
            {
                return (from order in dataContext.Orders
                          join customer in dataContext.Customers
                          on order.CustomerID equals customer.CustomerID
                          select new OrdersModel
                          {
                              OrderID = order.OrderID,
                              Address = order.ShipAddress,
                              City = order.ShipCity,
                              Region = order.ShipRegion,
                              CompanyName = customer.CompanyName,
                              ShippingCost = 0
                          }).ToList();
            }
        }
        private List<OrdersModel> GetMockOrders()
        {
            return new List<OrdersModel>
            {
                new OrdersModel()
                {
                    OrderID = 1234,
                    CompanyName = "Some company 1",
                    Address = "Road 101",
                    City = "SomeCity",
                    Region = "No",
                    ShippingCost = 0
                },
                new OrdersModel()
                {
                    OrderID = 4321,
                    CompanyName = "Some company 2",
                    Address = "Road 202",
                    City = "SomeCity",
                    Region = "Yes",
                    ShippingCost = 0
                }
            };
        }
    }
}
