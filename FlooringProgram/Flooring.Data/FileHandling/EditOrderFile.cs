using Flooring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.FileHandling
{
    public class EditOrderFile
    {
        public void EditOrderToFile(List<Order> orders, DateTime Date)
        {

            string filepath = FilePath.GetFilePath(Date);


            using (StreamWriter writer = new StreamWriter(filepath))
            {
                    writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                    foreach (var order in orders)
                    {
                        writer.WriteLine($"{order.OrderNumber},\"{order.CustomerName}\",{order.State},{order.TaxRate},{order.ProductType},{order.Area},{order.CostPerSquareFoot},{order.LaborCostPerSquareFoot},{order.MaterialCost},{order.LaborCost},{order.Tax},{order.Total}");
                    }
            }





        }
    }
}
