using Flooring.BLL;
using Flooring.Data.FileHandling;
using Flooring.Models;
using Flooring.Models.Helpers;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.WorkFlows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            DateTime orderDate = new DateTime().Date;
            Order order = new Order();
            List<Order> Orders = new List<Order>();
            OrderLookupResponse lookupResponse = new OrderLookupResponse();
            bool orderExists;
            EditOrderInFile editInFile = new EditOrderInFile();
            PrintReceipt print = new PrintReceipt();
            EditOrderFile editFile = new EditOrderFile();


            Console.Clear();
            Console.WriteLine("Edit an order");
            Console.WriteLine(TextHelper.ConsoleBar);
            Console.WriteLine("Please enter the following information....");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Date of order: ");
                if (DateTime.TryParse(Console.ReadLine(), out orderDate))
                {
                    order.Date = orderDate;
                    break;
                }
                Console.WriteLine("You did not enter a valid date format. Press any key to continue...");
                Console.ReadKey();
            }

            while (true)
            {
                lookupResponse = manager.LookupOrder(order.Date);
                if (lookupResponse != null)
                {
                    Orders = lookupResponse.Orders;
                }

                if (lookupResponse.success)
                    break;
                else
                {
                    Console.WriteLine("An error occurred.");
                    Console.WriteLine(lookupResponse.message);
                    continue;
                }

            }
            while (true)
            {
                Console.Clear();
                ConsoleIO.DisplayOrderDetails(lookupResponse.Orders);
                int orderNumber;
                Console.WriteLine("Order number: ");
                if (int.TryParse(Console.ReadLine(), out orderNumber))
                {
                    order.OrderNumber = orderNumber;
                }
                else
                {
                    Console.WriteLine("You did not enter a number. Press any key to continue...");
                    Console.ReadKey();

                }

                
                string filePath = FilePath.GetFilePath(order.Date);
                Orders = ReadOrdersFromFile.ReadOrders(filePath);
                orderExists = editInFile.CheckOrderNumberExists(Orders, order.OrderNumber);
                if (!orderExists)
                {
                    Console.WriteLine("That order number does not exist for the date you entered. Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    Order singleOrder = Orders.Where(ord => ord.OrderNumber == order.OrderNumber).Single();
                    order = singleOrder;
                }

                while (true)
                {
                    print.Print(order);
                    Console.WriteLine("Are you sure you want to delete this order? (Y/N)?");
                    string userInput = Console.ReadLine().ToUpper();
                    switch (userInput)
                    {
                        case "Y":
                            RemoveOrderFile remove = new RemoveOrderFile();
                            remove.RemoveOrderFromList(Orders, order, order.Date);
                            editFile.EditOrderToFile(Orders, order.Date);
                            Console.WriteLine("Remove has been saved! Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case "N":
                            Console.WriteLine("Remove has been canceled. Press any key to continue...");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("That was not a valid entry! Press any key to continue...");
                            Console.ReadKey();
                            continue;

                    }
                    break;
                }
                break;
            }
        }
    }  
}
