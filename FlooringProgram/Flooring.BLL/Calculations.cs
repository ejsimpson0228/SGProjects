using Flooring.BLL;
using Flooring.Data.FileHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Helpers
{
    public class Calculations
    {
        public Order MakeCalculations(Order Order, DateTime Date, string CustomerName, string State, string ProductType, decimal Area)
        {
            List<Taxes> Taxes = new List<Taxes>();
            Taxes = ReadFromTaxesFile.ReadTaxes(FilePaths.TaxesFilePath);
            List<Product> Products = new List<Product>();
            Products = ReadFromProductsFile.ReadProducts(FilePaths.ProductsFilePath);
            List<Order> Orders = new List<Order>();
            Orders = ReadOrdersFromFile.ReadOrders(FilePath.GetFilePath(Date));
            
            
            
                
            

            if (Orders.Count == 0)
            {
                Order.OrderNumber = 1;
            }
            else
            {
                var maxOrderNumber = Orders.Max(order => order.OrderNumber);
                Order.OrderNumber = maxOrderNumber + 1;
            }
            

            var stateTaxRate = Taxes.Where(state => state.StateAbbreviation.ToUpper() == State.ToUpper()).Select(state => state.TaxRate); //some kind of problem here when editing
            foreach (var tax in stateTaxRate)
            {
                Order.TaxRate = Math.Round(tax, 2);
                break;
            }

            var costPerSqFoot = Products.Where(product => product.ProductType.ToLower() == ProductType.ToLower()).Select(product => product.CostPerSquareFoot);
            foreach (var cost in costPerSqFoot)
            {
                Order.CostPerSquareFoot = Math.Round(cost, 2);
                break;
            }

            var laborCostPerSqFoot = Products.Where(product => product.ProductType.ToLower() == ProductType.ToLower()).Select(product => product.LaborCostPerSquareFoot);
            foreach (var cost in laborCostPerSqFoot)
            {
                Order.LaborCostPerSquareFoot = Math.Round(cost, 2);
                break;
            }

            Order.MaterialCost = Math.Round((Area * Order.CostPerSquareFoot), 2);
            Order.LaborCost = Math.Round((Area * Order.LaborCostPerSquareFoot), 2);
            Order.Tax = Math.Round(((Order.MaterialCost + Order.LaborCost) * (Order.TaxRate / 100)), 2);
            Order.Total = Math.Round((Order.MaterialCost + Order.LaborCost + Order.Tax), 2);

            return Order;
        }

        public Order EditCalculations(Order Order, DateTime Date, string CustomerName, string State, string ProductType, decimal Area)
        {
            List<Taxes> Taxes = new List<Taxes>();
            Taxes = ReadFromTaxesFile.ReadTaxes(FilePaths.TaxesFilePath);
            List<Product> Products = new List<Product>();
            Products = ReadFromProductsFile.ReadProducts(FilePaths.ProductsFilePath);
            List<Order> Orders = new List<Order>();
            Orders = ReadOrdersFromFile.ReadOrders(FilePath.GetFilePath(Date));

            var stateTaxRate = Taxes.Where(state => state.StateAbbreviation.ToUpper() == State.ToUpper()).Select(state => state.TaxRate); //some kind of problem here when editing
            foreach (var tax in stateTaxRate)
            {
                Order.TaxRate = Math.Round(tax, 2);
                break;
            }

            var costPerSqFoot = Products.Where(product => product.ProductType.ToLower() == ProductType.ToLower()).Select(product => product.CostPerSquareFoot);
            foreach (var cost in costPerSqFoot)
            {
                Order.CostPerSquareFoot = Math.Round(cost, 2);
                break;
            }

            var laborCostPerSqFoot = Products.Where(product => product.ProductType.ToLower() == ProductType.ToLower()).Select(product => product.LaborCostPerSquareFoot);
            foreach (var cost in laborCostPerSqFoot)
            {
                Order.LaborCostPerSquareFoot = Math.Round(cost, 2);
                break;
            }

            Order.MaterialCost = Math.Round((Area * Order.CostPerSquareFoot), 2);
            Order.LaborCost = Math.Round((Area * Order.LaborCostPerSquareFoot), 2);
            Order.Tax = Math.Round(((Order.MaterialCost + Order.LaborCost) * (Order.TaxRate / 100)), 2);
            Order.Total = Math.Round((Order.MaterialCost + Order.LaborCost + Order.Tax), 2);

            return Order;
        }
    }
}
