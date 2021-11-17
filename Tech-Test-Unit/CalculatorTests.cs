using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tech_Teste_Calculator;
using Tech_Teste_Calculator.Domain;
using Tech_Teste_Calculator.Domain.Entities;

namespace Tech_Test_Unit
{
    public class CalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Need_to_sum_two_values_​​correctly()
        {
            //arrange

            var calculator = new Calculators(new List<double>() { 2, 4, 5 }, Operators.SUM);
            var expected = 11;

            //act

            calculator.Execute();

            //assert

            Assert.AreEqual(expected, calculator.Result);
        }

        [Test]
        public void Need_to_subtract_two_values_​​correctly()
        {
            //arrange

            var calculator = new Calculators(new List<double>() { 10, 4, 1 }, Operators.SUBTRACT);
            var expected = 5;

            //act

            calculator.Execute();

            //assert

            Assert.AreEqual(expected, calculator.Result);
        }

        [Test]
        public void Need_to_divide_two_values_​​correctly()
        {
            //arrange

            var calculator = new Calculators(new List<double>() { 10, 2, 1 }, Operators.DIVIDER);
            var expected = 5;

            //act

            calculator.Execute();

            //assert

            Assert.AreEqual(expected, calculator.Result);
        }

        [Test]
        public void Need_to_multiply_two_values_​​correctly()
        {
            //arrange

            var calculator = new Calculators(new List<double>() { 10, 8, 2 }, Operators.MULTIPLY);
            var expected = 160;

            //act

            calculator.Execute();

            //assert

            Assert.AreEqual(expected, calculator.Result);
        }


        [Test]
        public void Need_to_execute_expression_correctly()
        {
            //arrange

            var calculator = new Calculators("1+(3*4)");
            var expected = 13;

            //act

            calculator.ExecuteAdvancedExpression();

            //assert

            Assert.AreEqual(expected, calculator.Result);
        }

        [Test]
        public void Check_if_throw_excpetion_if_not_pass_calculator()
        {
            //arrange

            var calculator = default(Calculators);
            var service = new CalculatorService();

            //assert

            Assert.Throws<ArgumentNullException>(() => service.DefineWichCommand(calculator));
        }

        [Test]
        public void Check_execute_precedence_operator()
        {
            //arrange
            var calculator = new Calculators("1+(3*4)");
            var expected = 13;

            var service = new CalculatorService();

            //act

            var response = service.DefineWichCommand(calculator);

            //assert

            Assert.AreEqual(expected, response);
        }

        [Test]
        public void execute_expression_user_input_sum()
        {
            //arrange
            var calculator = new Calculators(new List<double>() { 2, 4, 5 }, Operators.SUM);
            var expected = 11;

            var service = new CalculatorService();

            //act

            var response = service.DefineWichCommand(calculator);

            //assert

            Assert.AreEqual(expected, response);
        }

        [Test]
        public void simulate_user_input_precendence_expression()
        {
            //arrange 
            var stBuilder = new StringBuilder();

            stBuilder.AppendLine("P");
            //stBuilder.AppendLine("2+1*(2-1)");
            //stBuilder.AppendLine("20%3");
            stBuilder.AppendLine("-20+(-1)");
            stBuilder.AppendLine("N");

            var stReader = new StringReader(stBuilder.ToString());
            Console.SetIn(stReader);

            var sw = new StringWriter();
            Console.SetOut(sw);

            var expectedValue = 3;

            //act

            Program.Main(Array.Empty<string>());

            var outputConsole = sw.ToString();

            //assert

            Assert.True(outputConsole.Contains($"Result is: {expectedValue}"));
        }

    }
}