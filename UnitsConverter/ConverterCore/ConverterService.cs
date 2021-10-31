using ConverterCore.Distance;
using ConverterCore.Temperature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterCore
{
    public class ConverterService : IConverterService
    {
        public readonly List<Type> UnitTypes = new List<Type>()
        {
            typeof(DistanceUnits),
            typeof(TemperatureUnits),
            typeof(DataUnits.DataUnits)
        };

        public double Convert(double inputAmount, Enum inputUnit, Enum outputUnit, int inputExponent, int outputExponent)
        {
            var inputType = GetUnitType(inputUnit);
            var outputType = GetUnitType(outputUnit);

            if (inputType != outputType)
                throw new ArgumentException($"Unit types for input and output must be same. Current input type: {inputType}, output type: {outputType}");

            if (inputType == typeof(DistanceUnits))
                return DistanceConverter.Convert(inputAmount, (DistanceUnits)inputUnit, (DistanceUnits)outputUnit, inputExponent, outputExponent);
            else if (inputType == typeof(TemperatureUnits))
                return TemperatureConverter.Convert(inputAmount, (TemperatureUnits)inputUnit, (TemperatureUnits)outputUnit, inputExponent, outputExponent);
            else if (inputType == typeof(DataUnits.DataUnits))
                return DataUnits.DataUnitsConverter.Convert(inputAmount, (DataUnits.DataUnits)inputUnit, (DataUnits.DataUnits)outputUnit, inputExponent, outputExponent);

            throw new NotImplementedException();
        }

        public List<Type> GetUnitTypes()
        {
            return UnitTypes;
        }

        private Type GetUnitType(Enum unit)
        {
            foreach (var unitType in UnitTypes)
            {
                var typeUnits = Enum.GetNames(unitType).ToList();
                bool inType = typeUnits.Any(u => unit.ToString().ToLower().Contains(u.ToLower()));
                if (inType) return unitType;
            }

            throw new ApplicationException($"Unknown unit in request: {unit}");
        }
    }
}
