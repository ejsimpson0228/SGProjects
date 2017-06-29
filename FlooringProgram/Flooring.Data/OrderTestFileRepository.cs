using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flooring.Models;
using Flooring.Data.FileHandling;
using System.IO;

namespace Flooring.Data
{
    public class OrderTestFileRepository : IOrderRepository
    {
        

        private string _filePath;

        private List<Order> _orders = new List<Order>();

        List<Order> IOrderRepository.LoadOrders(DateTime Date)
        {
            _filePath = FilePath.GetFilePath(Date);
            if (!File.Exists(_filePath))
                    return null;
            else
            {
                _orders = ReadOrdersFromFile.ReadOrders(_filePath);
                
                return _orders;
            }
            
        }

        public void SaveOrders(List<Order> orders)
        {
            orders = _orders;
        }
    }
}
