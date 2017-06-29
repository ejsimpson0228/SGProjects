using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Interfaces
{
    public interface IEditOrder
    {
        EditOrderResponse EditRules(DateTime OrderDate, string CustomerName, string State, string ProductType, decimal Area, EditOrderResponse Response);
    }
}
