
namespace Tech_Teste_Calculator.Domain.Entities
{
    public class Operators
    {
        private Operators(string value) { Value = value; }

        public string Value { get; private set; }

        public static Operators SUM { get { return new Operators("+"); } }
        public static Operators SUBTRACT { get { return new Operators("-"); } }
        public static Operators MULTIPLY { get { return new Operators("*"); } }
        public static Operators DIVIDER { get { return new Operators("/"); } }

        public static Operators Percent { get { return new Operators("%"); } }

        public static Operators PRECEDENCE { get { return new Operators("P"); } }
    }

}
