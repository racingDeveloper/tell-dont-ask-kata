using System;
using System.Collections.Generic;
using System.Text;

namespace TellDontAsk
{
    public static class DecimalExtensions
    {
        public static decimal setScale(this decimal d, int decimalPlaces)
        {
            return decimal.Round(d, decimalPlaces, MidpointRounding.AwayFromZero);
        }

        public static decimal divide(this decimal dividend, decimal divisor)
        {
            return dividend / divisor;
        }

        public static decimal multiply(this decimal multiplicand, decimal multiplier)
        {
            return multiplicand * multiplier;
        }

        public static decimal add(this decimal addend1, decimal addend2)
        {
            return addend1 + addend2;
        }
    }
}
