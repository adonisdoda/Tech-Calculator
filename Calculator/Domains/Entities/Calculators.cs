using System;
using System.Collections.Generic;
using System.Linq;
using Z.Expressions;

namespace Tech_Teste_Calculator.Domain.Entities
{
    public class Calculators
    {
        private Dictionary<Operators, Action> OperatorsCatalog => new()
        {
            { Operators.SUM, () => Sum() },
            { Operators.SUBTRACT, () => Subtract() },
            { Operators.MULTIPLY, () => Multiply() },
            { Operators.DIVIDER, () => Divider() }
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

        public List<double> Values { get; }

        public double Result { get; private set; }

        public string PrecedenceExpression { get; }

        public Operators OperatorCommands { get; }


        public void Execute()
        {
            OperatorsCatalog[OperatorCommands].Invoke();
        }

        public void ExecuteAdvancedExpression()
        {

            if (string.IsNullOrEmpty(PrecedenceExpression))
            {
                throw new ArgumentNullException(nameof(PrecedenceExpression));
            }

            Result = Eval.Execute<double>(PrecedenceExpression);
        }

        public void Sum() => Result = Values.Sum();

        public void Subtract() => Result = Values.Aggregate((f, l) => f - l);

        public void Multiply() => Result = Values.Aggregate((f, l) => f * l);

        public void Divider() => Result = Values.Aggregate((f, l) => f / l);

    }
}
