using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Currency
    {
        public string ValueString { get; set; }
        public string Denomination { get; set; }
        public string DenominationPlural { get; set; }
        public double Value
        {
            get
            {
                double val;
                double.TryParse(ValueString, out val);
                return val;
            }
        }
    }
}
