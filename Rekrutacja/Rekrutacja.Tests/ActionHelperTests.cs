using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Rekrutacja.Helpers;
using Soneta.Data.QueryDefinition;
using Xunit;
using Moq;
using Rekrutacja.Models;

namespace Rekrutacja.Tests
{
    public class ActionHelperTests
    {
        private const char _operatorDodawania = '+';
        private const char _operatorOdejmowania = '-';
        private const char _operatorMnozenia = '*';
        private const char _operatorDzielenia = '/';
        [Theory]
        [InlineData(5, 0, _operatorDodawania, 5)]
        [InlineData(5, -5, _operatorDodawania, 0)]
        [InlineData(5, 0, _operatorOdejmowania, 5)]
        [InlineData(5, -5, _operatorOdejmowania, 10)]
        [InlineData(5, 0, _operatorMnozenia, 0)]
        [InlineData(5, -5, _operatorMnozenia, -25)]
        [InlineData(5, 0, _operatorDzielenia, double.PositiveInfinity)]
        [InlineData(5, -5, _operatorDzielenia, -1)]
        [InlineData(-5, 0, _operatorDzielenia, double.NegativeInfinity)]
        public void ObliczWynik_ForValidInputs_ReturnsCorrectResult(double valueA, double valueB, char actionOperator, double wynik)
        {
            //act
            var result = ActionHelper.ObliczWynik(valueA, valueB, actionOperator);
            
            //assert
            result.Should().Be(wynik);
        }
        [Theory]
        [InlineData('5')]
        [InlineData('m')]
        public void ObliczWynik_ForInvalidOperator_ReturnsException(char actionOperator)
        {
            //act
            Action action = () => ActionHelper.ObliczWynik(It.IsAny<double>(), It.IsAny<double>(), actionOperator);

            //assert
            action.Should().Throw<Exception>();
        }
        [Theory]
        [InlineData(2, 0, FigureType.Kwadrat, 4)]
        [InlineData(4, -5, FigureType.Kwadrat, 16)]
        [InlineData(5, 1, FigureType.Prostokąt, 5)]
        [InlineData(3, 4, FigureType.Prostokąt, 12)]
        [InlineData(5, 5, FigureType.Trojkat, 12)]
        [InlineData(3, 4, FigureType.Trojkat, 6)]
        [InlineData(5, 0, FigureType.Kolo, 79)]
        [InlineData(4, -5, FigureType.Kolo, 50)]
        public void ObliczPole_ForValidInputs_ReturnsCorrectResult(double valueA, double valueB, FigureType figureType, int wynik)
        {
            //act
            var result = ActionHelper.ObliczPole(valueA, valueB, figureType);

            //assert
            result.Should().Be(wynik);
        }
        [Theory]
        [InlineData(0, 0, FigureType.Kwadrat)]
        [InlineData(-5, 5, FigureType.Kwadrat)]
        [InlineData(-5, 5, FigureType.Prostokąt)]
        [InlineData(5, 0, FigureType.Prostokąt)]
        [InlineData(5, 0, FigureType.Trojkat)]
        [InlineData(-5, 5, FigureType.Trojkat)]
        [InlineData(0, 0, FigureType.Kolo)]
        [InlineData(-5, 5, FigureType.Kolo)]
        public void ObliczPole_ForInvalidData_ReturnsException(double valueA, double valueB, FigureType figureType)
        {
            //act
            Action action = () => ActionHelper.ObliczPole(valueA, valueB, figureType);

            //assert
            action.Should().Throw<Exception>();
        }
        [Theory]
        [InlineData("1", 1)]
        [InlineData("123", 123)]
        [InlineData("123456789", 123456789)]
        [InlineData("", 0)]
        [InlineData(null, 0)]
        public void ParseForIntValue_ForValidInputs_ReturnsCorrectResult(string stringValue, int wynik)
        {
            //act
            var result = stringValue.ParseForIntValue();

            //assert
            result.Should().Be(wynik);
        }
        [Theory]
        [InlineData("tekst")]
        [InlineData("123 456")]
        [InlineData("123/456")]
        public void ParseForIntValue_ForInValidInputs_ReturnsException(string stringValue)
        {
            //act
            Action action = () => stringValue.ParseForIntValue();

            //assert
            action.Should().Throw<Exception>();
        }
    }
}
