using System;

namespace Tech_Teste_Calculator.Commons
{
    public static class HandlerException
    {

        public static void HandleWichExcpetion(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            if (message.StartsWith("Missing left expression", StringComparison.InvariantCultureIgnoreCase) || message.StartsWith("Oops! No applicable member has been found", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Invalid expression, maybe you wrong have some input. Would you like to try another expression?");
            }
            else if (message.StartsWith("Oops! A null expression", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("It is not a valid expression. Would you like to try another expression?");
            }
            else
            {
                Console.WriteLine("Unexpected error ocurred, sorry.");
            }

        }



    }
}
