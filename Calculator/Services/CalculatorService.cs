using System;
using Tech_Teste_Calculator.Domain.Entities;
using Tech_Teste_Calculator.Domain.Interfaces;

namespace Tech_Teste_Calculator.Domain
{

    public class CalculatorService : ICalculatorService
    {
        public double DefineWichCommand(Calculators calculator)
        {

            if (calculator is null)
            {
                throw new ArgumentNullException(nameof(calculator));
            }

            if (calculator.OperatorCommands.Value == Operators.PRECEDENCE.Value)
            {
                calculator.ExecuteAdvancedExpression();
            }
            else
            {
                calculator.Execute();
            }


            return calculator.Result;

        }
    }
}


