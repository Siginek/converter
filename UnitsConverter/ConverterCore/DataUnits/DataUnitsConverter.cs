using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterCore.DataUnits
{
    internal class DataUnitsConverter
    {
        internal static double Convert(double amount, DataUnits inputUnit, DataUnits outputUnit, int inputExponent, int outputExponent)
        {
            if (inputExponent < 0 || outputExponent < 0)
                throw new ArgumentOutOfRangeException("Data units exponents must be greater or equal to zero");

            double byteRatio = 8;

            double baseUnit = inputUnit switch
            {
                DataUnits.Bit => amount,
                DataUnits.Byte => amount * byteRatio,
                _ => throw new ArgumentOutOfRangeException($"Unimplemented data unit - {inputUnit}"),
            };

            var result = outputUnit switch
            {
                DataUnits.Bit => baseUnit,
                DataUnits.Byte => baseUnit / byteRatio,
                _ => throw new ArgumentOutOfRangeException($"Unimplemented data unit - {outputUnit}"),
            };

            return result *= Math.Pow(10, inputExponent - outputExponent);
        }
    }
}
