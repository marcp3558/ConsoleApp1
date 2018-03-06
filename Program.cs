using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Currencies.Initialize();

            var transList = new List<Transaction>();

            transList.Add(new Transaction(cost: new decimal(3.33), amtTendered: new decimal(5.00)));

            foreach (var tran in transList)
            {
                tran.CalculateChange();
                tran.DisplayChange();
            }
        }
    }
}
