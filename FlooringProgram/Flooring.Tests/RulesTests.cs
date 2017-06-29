using Flooring.BLL.Rules;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Tests
{
    [TestFixture]
    public class RulesTests
    {
        
        [TestCase("7/25/1975", "Bob Smith", "PA", "Laminate", 120.00, false)]
        [TestCase("8/8/2019", "Bob Smith", "KY", "Wood", 600.00, false)]
        [TestCase("8/8/2019", "Bob Smith", "IN", "Bouncy House Floor", 800.00, false)]
        [TestCase("8/8/2019", "Bob Smith", "MI", "Carpet", 90.00, false)]
        [TestCase("8/8/2019", "Bob Smith", "OH", "Tile", 450.00, true)]
        public void AddRuleTest(DateTime OrderDate, string CustomerName, string State, string ProductType, decimal Area, bool expectedResponse)
        {
            AddOrderResponse actual = new AddOrderResponse();
            IAddOrder Add = new AddOrderRule();
            actual = Add.AddRules(OrderDate, CustomerName, State, ProductType, Area, actual);

            Assert.AreEqual(expectedResponse, actual.success);
        }


        [TestCase("7/25/1975", "Bob Smith", "PA", "Laminate", 120.00, true)]
        [TestCase("8/8/2019", "Bob Smith", "KY", "Wood", 600.00, false)]
        [TestCase("8/8/2019", "Bob Smith", "IN", "Bouncy House Floor", 800.00, false)]
        [TestCase("8/8/2019", "Bob Smith", "MI", "Carpet", 90.00, false)]
        [TestCase("8/8/2019", "Bob Smith", "OH", "Tile", 450.00, true)]
        public void EditRuleTest(DateTime OrderDate, string CustomerName, string State, string ProductType, decimal Area, bool expectedResponse)
        {
            EditOrderResponse actual = new EditOrderResponse();
            IEditOrder Edit = new EditOrderRule();
            actual = Edit.EditRules(OrderDate, CustomerName, State, ProductType, Area, actual);

            Assert.AreEqual(expectedResponse, actual.success);
        }
    }
}
