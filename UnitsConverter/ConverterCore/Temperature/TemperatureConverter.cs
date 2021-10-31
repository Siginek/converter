using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterCore.Temperature
{
    public class TemperatureConverter
    {
        internal static double Convert(double amount, TemperatureUnits inputUnit, TemperatureUnits outputUnit, int inputExponent, int outputExponent)
        {
            double inputAmount = amount * Math.Pow(10, inputExponent);

            double baseUnit = inputUnit switch
            {
                TemperatureUnits.Celsius => inputAmount,
                TemperatureUnits.Fahrenheit => (inputAmount - 32)*5/9,
                _ => throw new ArgumentOutOfRangeException($"Unimplemented temperature unit - {inputUnit}")
            };

            var result = outputUnit switch
            {
                TemperatureUnits.Celsius => baseUnit,
                TemperatureUnits.Fahrenheit => (baseUnit*9)/5+32,
                _ => throw new ArgumentOutOfRangeException($"Unimplemented temperature unit - {outputUnit}"),
            };

            // there might be check to not convert below absolute zero, but that depends on requirements

            result /= Math.Pow(10, outputExponent);
            return result;
        }
    }
}
