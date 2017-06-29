using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.FileHandling
{
    public class FilePath
    {
        public static string GetFilePath(DateTime Date)
        {
            string Folder = "";
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            if (mode == "TestFile")
                Folder = "TestFiles";
            if (mode == "ProdFile")
                Folder = "ProdFiles";
         

            string date = Date.ToString("MMddyyyy");

            string filePath = $"C:\\Users\\ejsim\\OneDrive\\Documents\\The Software Guild\\Repos\\Bitbucket\\" +
            $"eric-simpson-individual-work\\SGBank\\FlooringMastery\\Flooring.Data\\{Folder}\\" + $"Orders_{date}.txt";
            
            return filePath;

        }
    }
}
