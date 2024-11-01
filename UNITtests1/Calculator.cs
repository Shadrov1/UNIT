using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNITtests1
{
    public class Calculator
    {
        private readonly ILoggingService _loggingService;
        private readonly ICurrencyConverter _currencyConverter;

        public Calculator(ILoggingService loggingService, ICurrencyConverter currencyConverter)
        {
            _loggingService = loggingService;
            _currencyConverter = currencyConverter;
        }

        public double Divide(double numerator, double denominator)
        {
            if (denominator == 0)
            {
                _loggingService.Log("Попробуйте разделить на ноль.");
                throw new DivideByZeroException("Деление на ноль невозможно.");
            }

            double result = numerator / denominator;
            _loggingService.Log($"Деление выполнено: {numerator} / {denominator} = {result}");
            return result;
        }

        public double Multiply(double value1, double value2)
        {
            double result = value1 * value2;
            _loggingService.Log($"Умножение выполнено: {value1} * {value2} = {result}");
            return result;
        }

        public double Add(double value1, double value2)
        {
            double result = value1 + value2;
            _loggingService.Log($"Сложение выполнено: {value1} + {value2} = {result}");
            return result;
        }

        public double ConvertAndDivide(double amount, string fromCurrency, string toCurrency, double denominator)
        {
            double convertedAmount = _currencyConverter.Convert(amount, fromCurrency, toCurrency);
            return Divide(convertedAmount, denominator);
        }

        public double ConvertAndMultiply(double amount, string fromCurrency, string toCurrency, double multiplier)
        {
            double convertedAmount = _currencyConverter.Convert(amount, fromCurrency, toCurrency);
            return Multiply(convertedAmount, multiplier);
        }
    }

}
