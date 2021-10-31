using ConverterCore;
using ConverterCore.Distance;
using ConverterCore.Temperature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace UnitsConverter
{
    public class ConvertProcessor
    {

        private Dictionary<string, int> ExponentValues = new Dictionary<string, int>()
        {
            ["yotta"] = 24,
            ["zetta"] = 21,
            ["exa"] = 18,
            ["peta"] = 15,
            ["tera"] = 12,
            ["giga"] = 9,
            ["mega"] = 6,
            ["kilo"] = 3,
            ["hecto"] = 2,
            ["deca"] = 1,
            ["deci"] = -1,
            ["centi"] = -2,
            ["milli"] = -3,
            ["micro"] = -6,
            ["nano"] = -9,
            ["pico"] = -12,
            ["femto"] = -15,
            ["atto"] = -18,
            ["zepto"] = -21,
            ["yocto"] = -24
        };

        private readonly IConverterService _service;

        public ConvertProcessor(IConverterService service)
        {
            _service = service;
        }

        public string ProcessConversion(string inputValue, string outputValue)
        {
            var amount = GetAmount(inputValue);
            if (!amount.HasValue)
                throw new ArgumentException($"Unable to determine amount to convert. Current input: {inputValue}");

            var inputUnitType = GetUnitType(inputValue);
            var outputUnitType = GetUnitType(outputValue);

            var inputUnit = GetUnit(inputValue);
            var outputUnit = GetUnit(outputValue);

            var inputExponent = GetExponent(inputValue);
            var outputExponent = GetExponent(outputValue);

            if (inputUnitType != outputUnitType)
                throw new ArgumentException($"Unit types for input and output must be same. Current input type: {inputUnitType}, output type: {outputUnitType}");

            var result = _service.Convert(amount.Value, inputUnit, outputUnit, inputExponent, outputExponent);
            return String.Format("{0} {1}", Math.Round(result, 3), outputValue);
        }

        private double? GetAmount(string input)
        {
            var doubles = Regex.Split(input, @"[^0-9\.\,]+")
                .Where(d => d != string.Empty).ToList();

            var isValid = Double.TryParse(doubles.SingleOrDefault(), out double value);

            if (isValid)
                return value;

            return null;
        }

        private int GetExponent(string value)
        {
            var result = ExponentValues.SingleOrDefault(e => value.ToLower().Contains(e.Key.ToLower()));

            return result.Value;
        }

        private Type GetUnitType(string strUnit)
        {
            foreach (var unitType in _service.GetUnitTypes())
            {
                var typeUnits = Enum.GetNames(unitType).ToList();
                bool inType = typeUnits.Any(u => strUnit.ToLower().Contains(u.ToLower()));
                if (inType) return unitType;
            }

            throw new ApplicationException($"Unknown unit in request: {strUnit}");
        }

        private Enum GetUnit(string strUnit)
        {
            foreach (var unitType in _service.GetUnitTypes())
            {
                var typeUnits = Enum.GetNames(unitType).ToList();
                var unit = typeUnits.Where(u => strUnit.ToLower().Contains(u.ToLower())).SingleOrDefault();
                if (unit != null)
                    return (Enum)Enum.Parse(unitType, unit);
            }

            throw new ApplicationException($"Unknown unit in request: {strUnit}");
        }
    }
}
