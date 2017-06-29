using Flooring.Models;
using Flooring.Models.Responses;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Flooring.Data.FileHandling
{
    public class ReadOrdersFromFile
    {
        public static List<Order> ReadOrders(string filePath)
        {
            
            List<Order> orders = new List<Order>();
            if (!File.Exists(filePath))
            {
                return orders;
            }

            
                string end = filePath.Substring(filePath.Length - 12, 8);
                string date = end.Insert(2, "/");
                date = date.Insert(5, "/");

                string[] rows = File.ReadAllLines(filePath);

                for (int i = 1; i < rows.Length; i++)
                {
                string[] columns;

                    using (TextFieldParser parser = new TextFieldParser(new StringReader(rows[i])))
                    {
                        parser.HasFieldsEnclosedInQuotes = true;
                        parser.SetDelimiters(",");

                        columns = parser.ReadFields();
                    
                    }

                    Order o = new Order();
                    o.OrderNumber = int.Parse(columns[0]);
                    o.CustomerName = columns[1];
                    o.State = columns[2];
                    o.TaxRate = decimal.Parse(columns[3]);
                    o.ProductType = columns[4];
                    o.Area = decimal.Parse(columns[5]);
                    o.CostPerSquareFoot = decimal.Parse(columns[6]);
                    o.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    o.MaterialCost = decimal.Parse(columns[8]);
                    o.LaborCost = decimal.Parse(columns[9]);
                    o.Tax = decimal.Parse(columns[10]);
                    o.Total = decimal.Parse(columns[11]);
                    o.Date = DateTime.Parse(date);

                    orders.Add(o);
                }
           
                
            
                return orders;
            
            
            
        }
    }
}
