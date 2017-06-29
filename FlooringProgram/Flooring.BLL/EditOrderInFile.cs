using Flooring.Data.FileHandling;
using Flooring.Models;
using Flooring.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class EditOrderInFile
    {
        public bool CheckOrderNumberExists(List<Order> Orders, int OrderNumber)
        {
            foreach (var order in Orders)
            {
                if (order.OrderNumber == OrderNumber)
                {
                    return true;
                }
                else
                    continue;
            }
            return false;
        }

        public Order GetNewOrderInfoFromUser(Order order)
        {
            PrintReceipt print = new PrintReceipt();
            Console.Clear();
            print.Print(order);
            Console.WriteLine("Please update the following info. If no change is needed for a field, leave the field blank and press enter.");
            Console.WriteLine(TextHelper.ConsoleBar);
            Console.WriteLine();

            
            Console.WriteLine("Customer Name: ");
            string userInput = Console.ReadLine();
                
            if (!String.IsNullOrWhiteSpace(userInput))
            {
                order.CustomerName = userInput;
            }

            List<Taxes> taxes = new List<Taxes>();
            taxes = ReadFromTaxesFile.ReadTaxes(FilePaths.TaxesFilePath);
            Console.WriteLine("We service the states of...");
            foreach (var tax in taxes)
            {
                Console.Write(tax.StateAbbreviation + " ");
            }
            Console.WriteLine("\nPlease enter your state (use abbreviation): ");
            userInput = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(userInput))
            {
                order.State = userInput.ToUpper();
            }
            

            List<Product> products = new List<Product>();
            products = ReadFromProductsFile.ReadProducts(FilePaths.ProductsFilePath);
            Console.WriteLine("Products currently in stock:");
            foreach (var product in products)
            {
                Console.Write(product.ProductType + " ");
            }

            Console.WriteLine("\nPlease enter your product selection:");
            userInput = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(userInput))
            {
                order.ProductType = userInput;
            }
            

            while (true)
            {
                Console.WriteLine("Enter the area of the floor (sq ft): ");
                userInput = Console.ReadLine();
                decimal area;
                if (!String.IsNullOrWhiteSpace(userInput))
                {
                    if (decimal.TryParse(userInput, out area))
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
                break;
            }
            return order;
        }

        
    }
}
