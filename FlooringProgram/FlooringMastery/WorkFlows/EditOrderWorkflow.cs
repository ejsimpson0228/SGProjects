using Flooring.BLL;
using Flooring.BLL.Rules;
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
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order();
            bool orderExists;
            EditOrderInFile editInFile = new EditOrderInFile();
            DateTime orderDate = new DateTime().Date;
            Calculations caluclate = new Calculations();
            PrintReceipt print = new PrintReceipt();
            OrderLookupResponse lookupResponse = new OrderLookupResponse();
            EditOrderResponse editResponse = new EditOrderResponse();
            EditOrderRule editRules = new EditOrderRule();
            List<Order> Orders = new List<Order>();
            bool exception = false;
            
            

            while (true)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Edit an order");
                    Console.WriteLine(TextHelper.ConsoleBar);
                    Console.WriteLine("Please enter the following information....");
                    Console.WriteLine();

                    Console.WriteLine("Date of order: ");
                    if (DateTime.TryParse(Console.ReadLine(), out orderDate))
                    {
                        order.Date = orderDate;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You did not enter a valid date format. Press any key to continue...");
                        Console.ReadKey();

                        Console.Clear();
                    }
                }
  
                
            
                try
                {
                    lookupResponse = manager.LookupOrder(order.Date);
                    if (lookupResponse != null)
                    {
                        Orders = lookupResponse.Orders;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("You cannot proceed. Please contact IT.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    exception = true;
                    break;
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

            if (exception == false)
            {
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
                    order = editInFile.GetNewOrderInfoFromUser(order);
                    order = caluclate.EditCalculations(order, order.Date, order.CustomerName, order.State, order.ProductType, order.Area);
                    editResponse = editRules.EditRules(order.Date, order.CustomerName, order.State, order.ProductType, order.Area, editResponse);

                    if (!editResponse.success)
                    {
                        Console.WriteLine("An error occurred: ");
                        Console.WriteLine(editResponse.message);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                        break;
                }




                while (true)
                {
                    print.Print(order);

                    Console.WriteLine("Is the updated order correct (Y/N)?");
                    string userInput = Console.ReadLine().ToUpper();
                    switch (userInput)
                    {
                        case "Y":
                            EditOrderFile editOrder = new EditOrderFile();
                            editOrder.EditOrderToFile(Orders, order.Date);
                            Console.WriteLine("Edit has been saved! Press any key to continue...");
                            Console.ReadKey();
                            break;
                        case "N":
                            Console.WriteLine("Edit has been canceled. Press any key to continue...");
                            Console.ReadKey();
                            break;
                        default:
                            Console.WriteLine("That was not a valid entry! Press any key to continue...");
                            Console.ReadKey();
                            continue;

                    }
                    break;
                }
            }
            


        }
    }
}
