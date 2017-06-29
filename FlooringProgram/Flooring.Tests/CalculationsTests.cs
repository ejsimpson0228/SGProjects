using Flooring.Models;
using Flooring.Models.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Tests
{
    [TestFixture]
    public class CalculationsTests
    {
        [TestCase("PA", "Tile", 800.00, 2800.00, 3320.00, 413.10, 6533.10)]
        [TestCase("OH", "Carpet", 500.00, 1125.00, 1050.00, 135.94, 2310.94)]
        [TestCase("MI", "Laminate", 700.00, 1225.00, 1470.00, 154.96, 2849.96)]
        [TestCase("IN", "Wood", 900.00, 4635.00, 4275.00, 534.60, 9444.60)]
        public void CalculationsAreCorrect (string State, string ProductType, decimal Area, 
            decimal expectedMaterialCost, decimal expectedLaborCost, decimal expectedTax, decimal expectedTotal)
            {
                Calculations calculations = new Calculations();
                Order order = new Order();
                order.State = State;
                order.ProductType = ProductType;
                order.Area = Area;

                order = calculations.MakeCalculations(order, order.Date, order.CustomerName, State, ProductType, Area);

                Assert.AreEqual(expectedMaterialCost, order.MaterialCost);
                Assert.AreEqual(expectedLaborCost, order.LaborCost);
                Assert.AreEqual(expectedTax, order.Tax);
                Assert.AreEqual(expectedTotal, order.Total);
            }
    }
}
