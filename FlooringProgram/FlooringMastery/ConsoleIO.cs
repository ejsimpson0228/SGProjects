using Flooring.Models;
using Flooring.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails (List<Order> orders)
        {
            Console.Clear();
            foreach (var order in orders)
            {
                Console.WriteLine(TextHelper.ConsoleBar);
                Console.WriteLine();
                Console.WriteLine($"[{order.OrderNumber}] [{order.Date.ToShortDateString()}]");
                Console.WriteLine($"[{order.CustomerName}]");
                Console.WriteLine($"[{order.State}]");
                Console.WriteLine($"Product: [{order.ProductType}]");
                Console.WriteLine($"Materials: [{order.MaterialCost}]");
                Console.WriteLine($"Labor: [{order.LaborCost}]");
                Console.WriteLine($"Tax: [{order.Tax}]");
                Console.WriteLine($"Total: [{order.Total}]");
                Console.WriteLine();
                Console.WriteLine(TextHelper.ConsoleBar);
            }

        }
    }
}
