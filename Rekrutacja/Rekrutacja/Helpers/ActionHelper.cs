using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekrutacja.Helpers
{
    public static class ActionHelper
    {
        public static double ObliczWynik(double valueA, double valueB, char actionOperator)
        {
            double wynik = 0;
            if (actionOperator.Equals('+'))
                wynik = valueA + valueB;
            else if (actionOperator.Equals('-'))
                wynik = valueA - valueB;
            else if (actionOperator.Equals('*'))
                wynik = valueA * valueB;
            else if (actionOperator.Equals('/'))
                wynik = valueA / valueB;
            else
                throw new Exception("Niepoprawny operator");
            return wynik;
        }
    }
}
