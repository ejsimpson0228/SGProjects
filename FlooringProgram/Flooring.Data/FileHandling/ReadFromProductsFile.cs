using Flooring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.FileHandling
{
    public class ReadFromProductsFile
    {
        public static List<Product> ReadProducts(string filePath)
        {
            string[] rows = File.ReadAllLines(filePath);

            List<Product> products = new List<Product>();

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Product product = new Product();

                product.ProductType = columns[0];
                product.CostPerSquareFoot = decimal.Parse(columns[1]);
                product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                products.Add(product);
            }

            return products;
        }
    }
}
