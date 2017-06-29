using Flooring.BLL.Rules;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(DateTime OrderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            response.Orders = _orderRepository.LoadOrders(OrderDate);

            if (response.Orders == null)
            {
                response.success = false;
                response.message = $"There are no orders found on  {OrderDate.Date.ToShortDateString()}.";
            }
            else
                response.success = true;
            return response;
            
        }

        public AddOrderResponse AddOrder(DateTime OrderDate, string CustomerName, string State, string ProductType, decimal Area)
        {
            AddOrderResponse response = new AddOrderResponse();

            response.Orders = _orderRepository.LoadOrders(OrderDate);

            IAddOrder addRule = new AddOrderRule();
            response = addRule.AddRules(OrderDate, CustomerName, State, ProductType, Area, response);

            return response;
        }
        
        public EditOrderResponse EditOrder(DateTime OrderDate, string CustomerName, string State, string ProductType, decimal Area)
        {
            EditOrderResponse response = new EditOrderResponse();

            response.Orders = _orderRepository.LoadOrders(OrderDate);

            IEditOrder editRule = new EditOrderRule();
            response = editRule.EditRules(OrderDate, CustomerName, State, ProductType, Area, response);

            return response;
        }

        
    }
}
