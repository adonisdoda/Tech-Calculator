using System;
using System.Collections.Generic;
using System.Linq;

namespace Tech_Teste_Calculator.Domain.Entities
{
    public class Calculators
    {
        private Dictionary<string, Func<List<double>, double>> OperatorsPrecendence => new(StringComparer.InvariantCultureIgnoreCase)
        {
            { Operators.SUM.Value, (List<double> parameters) => Sum(parameters) },
            { Operators.SUBTRACT.Value, (List<double> parameters) => Subtract(parameters) },
            { Operators.MULTIPLY.Value, (List<double> parameters) => Multiply(parameters) },
            { Operators.DIVIDER.Value, (List<double> parameters) => Divider(parameters) }
        };


        public Calculators(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            PrecedenceExpression = expression;
            OperatorCommands = Operators.PRECEDENCE;
        }

        public Calculators(List<double> values, Operators operatorCommand)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (!values.Any())
            {
                throw new ArgumentException("Cannot be an empty list");
            }

            Values = values;

            OperatorCommands = operatorCommand;
        }

        public List<double> Values { get; private set; }

        public double Result { get; private set; }

        public string PrecedenceExpression { get; private set; }

        public Operators OperatorCommands { get; }


        public void Execute()
        {
            Result = OperatorsPrecendence[OperatorCommands.Value].Invoke(Values);
        }

        public void ExecuteAdvancedExpression()
        {

            if (string.IsNullOrEmpty(PrecedenceExpression))
            {
                throw new ArgumentNullException(nameof(PrecedenceExpression));
            }

            Result = RecursiveCalculatePrecendence(Precendence.PolishOrder(PrecedenceExpression));
        }

        public double RecursiveCalculatePrecendence(Stack<string> ordered)
        {
            var actualVal = ordered.Pop();

            double right;

            if (!double.TryParse(actualVal, out double left))
            {
                right = RecursiveCalculatePrecendence(ordered);

                left = RecursiveCalculatePrecendence(ordered);

                if (OperatorsPrecendence.ContainsKey(actualVal))
                {
                    left = OperatorsPrecendence[actualVal].Invoke(new List<double>() { left, right });
                }
                else
                {
                    throw new ArgumentException("This operator is not mapped yet.");
                }
            }

            return left;
        }


        public double Sum(List<double> values) => values.Sum();

        public double Subtract(List<double> values) => values.Aggregate((f, l) => f - l);

        public double Multiply(List<double> values) => values.Aggregate((f, l) => f * l);

        public double Divider(List<double> values) => values.Aggregate((f, l) => f / l);

    }

}

