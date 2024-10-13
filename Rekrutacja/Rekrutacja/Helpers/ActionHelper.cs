using Rekrutacja.Models;
using System;
using System.CodeDom;
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
        public static int ObliczPole(double valueA, double valueB, FigureType typFigury)
        {
            
            switch (typFigury)
            {
                case FigureType.Kwadrat:
                    if (valueA <= 0)
                        throw new Exception("Wartość boku kwadratu musi być dodatnia!");
                    return (int)Math.Round(valueA * valueA);
                case FigureType.Prostokąt:
                    if (valueA <= 0 || valueB <= 0)
                        throw new Exception("Wartości boków prostokąta muszą być dodatnie!");
                    return (int)Math.Round(valueA * valueB);
                case FigureType.Trojkat:
                    if (valueA <= 0 || valueB <= 0)
                        throw new Exception("Wartości podstawy i wysokości trójkąta muszą być dodatnie!");
                    return (int)Math.Round(valueA * valueB/2);
                case FigureType.Kolo:
                    if (valueA <= 0)
                        throw new Exception("Wartość promienia koła musi być dodatnia!");
                    return (int)Math.Round(Math.PI*valueA * valueA);
                default:
                    throw new Exception("Nie wybrano typu figury");
            }
        }
    }
}
