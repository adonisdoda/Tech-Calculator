using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Tech_Teste_Calculator.Domain.Entities
{
    public static class Precendence
    {

        public static Stack<string> PolishOrder(string expressions)
        {
            var outStack = new Stack<string>();
            var tempStack = new Stack<string>();

            var splited = Regex.Matches(expressions, @"\d+(?:[,.]\d+)*(?:e[-+]?\d+)?|[-^+*/()]|\w+", RegexOptions.IgnoreCase)
              .Cast<Match>()
              .Select(p => p.Value)
              .ToList();

            foreach (var character in splited)
            {
                var isNumber = double.TryParse(character, out double n);

                if (isNumber)
                {
                    outStack.Push(character);
                }
                else if (character.Equals("(", StringComparison.InvariantCultureIgnoreCase))
                {
                    tempStack.Push(character);
                }
                else if (character.Equals(")", StringComparison.InvariantCultureIgnoreCase))
                {
                    while (!tempStack.Peek().Equals("(", StringComparison.InvariantCultureIgnoreCase))
                    {
                        outStack.Push(tempStack.Pop());
                    }

                    tempStack.Pop();
                }
                else
                {
                    while (tempStack.Any() && IsHigherPriority(tempStack.Peek(), character))
                    {
                        outStack.Push(tempStack.Pop());
                    }

                    tempStack.Push(character);
                }
            }

            while (tempStack.Any())
            {
                outStack.Push(tempStack.Pop());
            }

            return outStack;
        }


        private static bool IsHigherPriority(string comparer, string toCompare)
        {
            var priority = new Dictionary<string, int>()
            {
                {"(",0 },
                {"+",1 },
                {"-",1 }
            };

            int pr1, pr2;

            if (priority.ContainsKey(comparer))
            {
                pr1 = priority[comparer];
            }
            else
            {
                pr1 = 2;
            }

            if (priority.ContainsKey(toCompare))
            {
                pr2 = priority[toCompare];
            }
            else
            {
                pr2 = 2;
            }

            return pr1 >= pr2;

        }


    }

}


