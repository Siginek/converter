using System;

namespace ConverterCore.Distance
{
    internal class DistanceConverter
    {
        internal static double Convert(double amount, DistanceUnits inputUnit, DistanceUnits outputUnit, int inputExponent, int outputExponent)
        {
            double yardRatio = 0.9144;
            double inchRatio = 0.0254;
            double footRatio = 0.3048;

            double baseUnit = inputUnit switch
            {
                DistanceUnits.Meter => amount,
                DistanceUnits.Yard => amount * yardRatio,
                DistanceUnits.Inch => amount * inchRatio,
                var unit when unit == DistanceUnits.Foot || unit == DistanceUnits.Feet => amount * footRatio,
                _ => throw new ArgumentOutOfRangeException($"Unknown distance unit - {inputUnit}"),
            };

            var result = outputUnit switch
            {
                DistanceUnits.Meter => baseUnit,
                DistanceUnits.Yard => baseUnit / yardRatio,
                DistanceUnits.Inch => baseUnit / inchRatio,
                var unit when unit == DistanceUnits.Foot || unit == DistanceUnits.Feet => baseUnit / footRatio,
                _ => throw new ArgumentOutOfRangeException($"Unknown distance unit - {outputUnit}"),
            };

            return result *= Math.Pow(10, inputExponent - outputExponent);
        }
    }
}
