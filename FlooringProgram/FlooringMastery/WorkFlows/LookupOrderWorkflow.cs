using Flooring.BLL;
using Flooring.Models.Helpers;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.WorkFlows
{
    public class LookupOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            DateTime orderDate = new DateTime().Date;
            //bool Exception = false;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Look up an order");
                Console.WriteLine(TextHelper.ConsoleBar);
                Console.Write("Enter the date of the order: ");
                
            
                if (DateTime.TryParse(Console.ReadLine(), out orderDate))
                    break;
                
                Console.WriteLine("That was not a valid entry. Press any key to continue...");
                Console.ReadKey();
                
            }
            
            try
            {
                OrderLookupResponse response = manager.LookupOrder(orderDate.Date);

                if (response.success)
                    ConsoleIO.DisplayOrderDetails(response.Orders);
                else
                {
                    Console.WriteLine("An error occurred.");
                    Console.WriteLine(response.message);
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception Ex)
            {
                Console.WriteLine("An error occurred! " + Ex.Message);
                Console.WriteLine("Press any key to continue....");
                Console.ReadKey();
            }

        }
    }
}
