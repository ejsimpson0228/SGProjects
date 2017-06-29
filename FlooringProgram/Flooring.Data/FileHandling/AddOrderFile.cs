using Flooring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.FileHandling
{
    public class AddOrderFile
    {
        public void AddOrderToFile(List<Order> orders, Order Order, DateTime Date)
        {
            
            string filepath = FilePath.GetFilePath(Date);
            
            
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                
                if (orders == null)
                {
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    writer.WriteLine($"{Order.OrderNumber},\"{Order.CustomerName}\",{Order.State},{Order.TaxRate},{Order.ProductType},{Order.Area},{Order.CostPerSquareFoot},{Order.LaborCostPerSquareFoot},{Order.MaterialCost},{Order.LaborCost},{Order.Tax},{Order.Total}");

                }
                else
                {
                    orders.Add(Order);
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    foreach (var order in orders)
                    {
                        writer.WriteLine($"{order.OrderNumber},\"{order.CustomerName}\",{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}");
                    }
                }
            }
                


            
            
        }
    }
}
