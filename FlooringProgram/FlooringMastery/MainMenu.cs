using Flooring.Models.Helpers;
using FlooringMastery.WorkFlows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FlooringMastery
{
    public class MainMenu
    {
        public static void StartMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(TextHelper.ConsoleBar);

                Console.WriteLine("* Flooring Program");
                Console.WriteLine("*");
                Console.WriteLine("* 1. Display Orders");
                Console.WriteLine("* 2. Add an Order");
                Console.WriteLine("* 3. Edit an Order");
                Console.WriteLine("* 4. Remove an Order");
                Console.WriteLine("* 5. Quit");
                Console.WriteLine("*");
                Console.WriteLine(TextHelper.ConsoleBar);
                Console.WriteLine("Enter your selection: ");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        LookupOrderWorkflow lookupWorkflow = new LookupOrderWorkflow();
                        lookupWorkflow.Execute();
                        break;
                    case "2":
                        AddOrderWorkflow addOrderWorkflow = new AddOrderWorkflow();
                        addOrderWorkflow.Execute();
                        break;
                    case "3":
                        EditOrderWorkflow editOrderWorkflow = new EditOrderWorkflow();
                        editOrderWorkflow.Execute();
                        break;
                    case "4":
                        RemoveOrderWorkflow removeOrderWorkflow = new RemoveOrderWorkflow();
                        removeOrderWorkflow.Execute();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("That was not a valid entry! Press any key to contine...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
