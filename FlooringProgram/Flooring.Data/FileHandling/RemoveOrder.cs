using Flooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.FileHandling
{
    public class RemoveOrder
    {
        public List<Order> RemoveSingleOrder(List<Order> Orders, int OrderNumber)
        {
            Order SingleOrder = Orders.Where(order => order.OrderNumber == OrderNumber).Single();

            Orders.Remove(SingleOrder);

            return Orders;
            
        }
    }
}
