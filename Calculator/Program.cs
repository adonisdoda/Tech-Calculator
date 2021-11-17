using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Tech_Teste_Calculator.Commons;
using Tech_Teste_Calculator.Domain;
using Tech_Teste_Calculator.Domain.Entities;
using Tech_Teste_Calculator.Domain.Interfaces;

namespace Tech_Teste_Calculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var service = ConfigureServices();

            Console.WriteLine(@"
                  ____                   ___       _                    _    ____        _               _         _               
                 |  _ \  ___   ___  _ __|_ _| ___ | |  __ _  _ __    __| |  / ___| __ _ | |  ___  _   _ | |  __ _ | |_  ___   _ __ 
                 | |_) |/ _ \ / _ \| '__|| | / __|| | / _` || '_ \  / _` | | |    / _` || | / __|| | | || | / _` || __|/ _ \ | '__|
                 |  __/|  __/|  __/| |   | | \__ \| || (_| || | | || (_| | | |___| (_| || || (__ | |_| || || (_| || |_| (_) || |   
                 |_|    \___| \___||_|  |___||___/|_| \__,_||_| |_| \__,_|  \____|\__,_||_| \___| \__,_||_| \__,_| \__|\___/ |_|   ");


            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");

                    Console.WriteLine("Would you like to perform a simple(S) or precedence expression(P) ?");

                    var operationType = Console.ReadLine();
                    var calculator = default(Calculators);

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");

                    if (string.Equals("P", operationType, StringComparison.InvariantCultureIgnoreCase))
                    {

                        Console.WriteLine("Please, digit your expression: ");
                        Console.WriteLine(" ");

                        Console.ForegroundColor = ConsoleColor.White;

                        var expression = Console.ReadLine();

                        calculator = new Calculators(expression);
                    }
                    else if (string.Equals("S", operationType, StringComparison.InvariantCultureIgnoreCase))
                    {

                        Console.WriteLine("Would operation would you like: Sum (+) , Subtract (-) , Multiply (*) or Divider (/) ");

                        var operatorInput = Console.ReadLine();
                        var definedOperator = default(Operators);

                        Console.ForegroundColor = ConsoleColor.Yellow;

                        var hashOperators = new Dictionary<string, Operators>(StringComparer.InvariantCultureIgnoreCase) {
                            { Operators.SUM.Value, Operators.SUM },
                            { Operators.SUBTRACT.Value,Operators.SUBTRACT },
                            { Operators.DIVIDER.Value,Operators.DIVIDER},
                            { Operators.MULTIPLY.Value,Operators.MULTIPLY }
                        };

                        if (!hashOperators.ContainsKey(operatorInput))
                        {
                            Console.WriteLine(" ");

                            Console.WriteLine("Invalid input, try again");

                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.White;
                            continue;
                        }

                        definedOperator = hashOperators[operatorInput];

                        Console.WriteLine(" ");
                        Console.WriteLine("If you want to stop typing, press ';' ");
                        Console.WriteLine(" ");

                        Console.ForegroundColor = ConsoleColor.White;

                        var values = new List<double>();

                        while (true)
                        {
                            var userInput = Console.ReadLine();

                            if (string.Equals(userInput, ";", StringComparison.InvariantCultureIgnoreCase))
                            {
                                break;
                            }

                            if (double.TryParse(userInput, out double value))
                            {
                                values.Add(value);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(" ");
                                Console.WriteLine("Is not an number, please type only numbers");
                                Console.WriteLine(" ");
                                Console.ForegroundColor = ConsoleColor.White;

                            }

                        }

                        calculator = new Calculators(values, definedOperator);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine(" ");
                        Console.WriteLine("Invalid input, try again");
                        Console.WriteLine(" ");

                        continue;
                    }

                    var response = service.DefineWichCommand(calculator);

                    Console.ForegroundColor = ConsoleColor.Green;


                    Console.WriteLine(" ");
                    Console.WriteLine($"Result is: {response}");
                    Console.WriteLine(" ");

                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("Would you like to continue using the calculator ? If not press N");
                    var continueUsing = Console.ReadLine();

                    if (string.Equals("N", continueUsing, StringComparison.InvariantCultureIgnoreCase))
                    {
                        break;
                    }

                }
                catch (Exception error)
                {
                    HandlerException.HandleWichExcpetion(error.Message);
                    Console.WriteLine(" ");
                    Console.WriteLine("Press ENTER");
                    Console.ReadKey();
                }
            }
        }

        private void ExecuteCalculate()
        {

        }


        //DI
        public static ICalculatorService ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICalculatorService, CalculatorService>()
                .BuildServiceProvider();

            return serviceProvider.GetService<ICalculatorService>();

        }
    }
}
