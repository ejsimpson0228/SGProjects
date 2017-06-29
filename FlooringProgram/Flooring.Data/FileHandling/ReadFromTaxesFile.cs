using Flooring.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data.FileHandling
{
    public class ReadFromTaxesFile
    {
        public static List<Taxes> ReadTaxes(string filePath)
        {
            string[] rows = File.ReadAllLines(filePath);

            List<Taxes> taxes = new List<Taxes>();

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Taxes tax = new Taxes();
                tax.StateAbbreviation = columns[0];
                tax.StateName = columns[1];
                tax.TaxRate = decimal.Parse(columns[2]);

                taxes.Add(tax);
            }

            return taxes;
        }
    }
}
