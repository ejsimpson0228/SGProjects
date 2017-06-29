using Flooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.FileHandling
{
    public class RemoveOrderFile
    {
        public List<Order> RemoveOrderFromList(List<Order> Orders, Order Order, DateTime Date)
        {
            string filePath = FilePath.GetFilePath(Date);

            RemoveOrder remove = new RemoveOrder();
            Orders = remove.RemoveSingleOrder(Orders, Order.OrderNumber);

            return Orders;
        }
    }
}
