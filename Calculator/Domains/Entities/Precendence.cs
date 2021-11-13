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
                var parseCharac = character.ToString();

                if (string.IsNullOrWhiteSpace(parseCharac))
                {
                    continue;
                }

                var isNumber = double.TryParse(parseCharac, out double n);

                if (isNumber)
                {
                    outStack.Push(parseCharac);
                }
                else if (parseCharac.Equals("(", StringComparison.InvariantCultureIgnoreCase))
                {
                    tempStack.Push(parseCharac);
                }
                else if (parseCharac.Equals(")", StringComparison.InvariantCultureIgnoreCase))
                {
                    while (!tempStack.Peek().Equals("(", StringComparison.InvariantCultureIgnoreCase))
                    {
                        outStack.Push(tempStack.Pop());
                    }

                    tempStack.Pop();
                }
                else
                {
                    while (tempStack.Any() && IsHigherPriority(tempStack.Peek(), parseCharac))
                    {
                        outStack.Push(tempStack.Pop());
                    }

                    tempStack.Push(parseCharac);
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


