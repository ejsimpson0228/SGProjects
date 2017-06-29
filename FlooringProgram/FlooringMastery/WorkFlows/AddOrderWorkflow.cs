using Flooring.BLL;
using Flooring.BLL.Rules;
using Flooring.Data.FileHandling;
using Flooring.Models;
using Flooring.Models.Helpers;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.WorkFlows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();
            Order order = new Order();
            DateTime orderDate = new DateTime().Date;
            Calculations calculate = new Calculations();
            PrintReceipt print = new PrintReceipt();
            AddOrderResponse response = new AddOrderResponse();
            

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add an order");
                Console.WriteLine(TextHelper.ConsoleBar);
                Console.WriteLine("Please enter the following information....");
                Console.WriteLine();

                while (true)
                {
                    Console.WriteLine("Date of Order: ");
                    if (DateTime.TryParse(Console.ReadLine(), out orderDate))
                    {
                        order.Date = orderDate;
                        break;
                    }

                    Console.WriteLine("That was not a valid date. Press any key to continue...");
                    Console.ReadKey();
                }

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Customer Name: ");
                    string customerName = Console.ReadLine();
                    
                    order.CustomerName = customerName;
                    
                    if (String.IsNullOrWhiteSpace(order.CustomerName))
                    {
                        Console.WriteLine("You did not enter anything! Press any key to continue...");
                        Console.ReadKey();
                    }
                   
                    else
                        break;

                }

                List<Taxes> taxes = new List<Taxes>();
                taxes = ReadFromTaxesFile.ReadTaxes(FilePaths.TaxesFilePath);
                Console.Clear();
                Console.WriteLine("We service the states of...");
                foreach (var tax in taxes)
                {
                    Console.WriteLine(tax.StateAbbreviation + " ");
                }
                Console.WriteLine("\nPlease enter your state (use abbreviation): ");
                order.State = Console.ReadLine().ToUpper();

                List<Product> products = new List<Product>();
                products = ReadFromProductsFile.ReadProducts(FilePaths.ProductsFilePath);
                Console.Clear();
                Console.WriteLine("Products currently in stock:");
                foreach (var product in products)
                {
                    Console.WriteLine(product.ProductType + " ");
                }

                Console.WriteLine("\nPlease enter your product selection:");
                order.ProductType = Console.ReadLine();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Enter the area of the floor (sq ft). Minimun order is 100: ");
                    decimal area;
                    if (decimal.TryParse(Console.ReadLine(), out area))
                    {
                        order.Area = area;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("That was not a valid entry! Press any key to continue...");
                        Console.ReadKey();
                        continue;
                    }


                }

                response = manager.AddOrder(order.Date, order.CustomerName, order.State, order.ProductType, order.Area);
                if (!response.success)
                {
                    Console.WriteLine("There was an error with your order...");
                    Console.WriteLine(response.message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
                else
                    break;
            }

            order = calculate.MakeCalculations(order, order.Date, order.CustomerName, order.State, order.ProductType, order.Area);
            while (true)
            {
                print.Print(order);
                Console.WriteLine("Are you sure you want to place this order (Y/N)? ");
                string userResponse = Console.ReadLine().ToUpper();
                switch (userResponse)
                {
                    case "Y":
                        AddOrderFile addOrder = new AddOrderFile();
                        addOrder.AddOrderToFile(response.Orders, order, order.Date);
                        Console.WriteLine("Order has been saved! Press any key to continue...");
                        Console.ReadKey();
                        break;
                    case "N":
                        Console.WriteLine("Order has been canceled. Press any key to continue...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("That was not a valid choice. Press any key to try again...");
                        Console.ReadKey();
                        continue;
                }
                break;
            }
            
            
        }
    }
}
