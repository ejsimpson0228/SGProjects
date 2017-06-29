using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Flooring.Data;
using Flooring.Models;

namespace Flooring.BLL
{
    public class OrderManagerFactory 
    {
        

        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            

            switch(mode)
            {
                case "TestRepo":
                    return new OrderManager(new OrderTestRepository());
                case "TestFile":
                    return new OrderManager(new OrderTestFileRepository());
                case "ProdFile":
                    return new OrderManager(new OrderProdFileRepository());
                default:
                    throw new Exception("Mode value app config is not valid.");
            }

        }
        
            
            
    
    

    }
}
