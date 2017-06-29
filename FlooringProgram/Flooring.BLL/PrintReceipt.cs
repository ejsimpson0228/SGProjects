using Flooring.Models;
using Flooring.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class PrintReceipt
    {
        public void Print(Order order)
        {
            Console.Clear();
            Console.WriteLine(TextHelper.ConsoleBar);
            Console.WriteLine("--ORDER RECEIPT--");
            Console.WriteLine("Order number: " + order.OrderNumber);
            Console.WriteLine("Name: " + order.CustomerName);
            Console.WriteLine("State: " + order.State);
            Console.WriteLine("Tax Rate: " + order.TaxRate.ToString());
            Console.WriteLine("Product Type: " + order.ProductType);
            Console.WriteLine("Area : " + order.Area.ToString() + "sq ft");
            Console.WriteLine("Cost per square foot: $" + order.CostPerSquareFoot.ToString());
            Console.WriteLine("Labor cost per square foot: $" + order.LaborCostPerSquareFoot.ToString());
            Console.WriteLine("Full cost of materials: $" + order.MaterialCost.ToString());
            Console.WriteLine("Full cost of labor: $" + order.LaborCost.ToString());
            Console.WriteLine("Tax: $" + order.Tax.ToString());
            Console.WriteLine();
            Console.WriteLine("TOTAL: $" + order.Total);
            Console.WriteLine();
            Console.WriteLine(TextHelper.ConsoleBar);

        }
    }
}
