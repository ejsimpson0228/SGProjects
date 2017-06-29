using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;

namespace Flooring.Data
{
    public class OrderTestRepository : IOrderRepository
    {
        private static Order _order = new Order
        {
            Date = new DateTime(2016,01,12),
            OrderNumber = 1,
            CustomerName = "Eric Simpson",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100.00M,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            MaterialCost = 515.00M,
            LaborCost = 475.00M,
            Tax = 61.88M,
            Total = 1051.88M
        };

        private List<Order> orders = new List<Order>();

        public List<Order> LoadOrders(DateTime Date)
        {
            if (Date != _order.Date)
                return null;
            else
            {
                orders.Add(_order);
                return orders;
            }
        }

        

        

        public void SaveOrders(List<Order> orders)
        {
            orders.Add(_order);

        }
    }
}
