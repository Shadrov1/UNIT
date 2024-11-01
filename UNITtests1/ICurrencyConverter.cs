using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNITtests1
{
    public interface ICurrencyConverter
    {
        double Convert(double amount, string fromCurrency, string toCurrency);
    }
}
