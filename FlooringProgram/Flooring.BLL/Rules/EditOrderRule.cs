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

namespace Flooring.BLL.Rules
{
    public class EditOrderRule : IEditOrder
    {
        public EditOrderResponse EditRules(DateTime OrderDate, string CustomerName, string State, string ProductType, decimal Area, EditOrderResponse Response)
        {
            Response.success = false;

            if (Area < 100)
            {
                Response.message = "Area of orders must be a minimun of 100.00 square feet.";
                return Response;
            }

            List<Taxes> taxes = new List<Taxes>();
            taxes = ReadFromTaxesFile.ReadTaxes(FilePaths.TaxesFilePath);
            foreach (var tax in taxes)
            {
                if (tax.StateAbbreviation.ToUpper() == State.ToUpper())
                {
                    Response.success = true;
                    break;
                }
                else
                {
                    Response.success = false;
                    continue;
                }
            }

            if (Response.success == false)
            {
                Response.message = "You did not enter a valid state abbreviation.";
                return Response;
            }

            List<Product> products = new List<Product>();
            products = ReadFromProductsFile.ReadProducts(FilePaths.ProductsFilePath);
            foreach (var product in products)
            {
                if (product.ProductType.ToLower() == ProductType.ToLower())
                {
                    Response.success = true;
                    break;
                }
                else
                {
                    Response.success = false;
                    continue;
                }

            }

            if (Response.success == false)
            {
                Response.message = "You did not enter a valid product type.";
                return Response;
            }

            return Response;
        }
    }
}
