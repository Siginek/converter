using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterCore
{
    public interface IConverterService
    {
        double Convert(double inputAmount, Enum inputUnit, Enum outputUnit, int inputExponent, int outputExponent);
        List<Type> GetUnitTypes();
    }
}
