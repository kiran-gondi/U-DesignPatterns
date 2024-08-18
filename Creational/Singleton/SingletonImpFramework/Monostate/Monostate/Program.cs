
namespace DesignPatterns
{
    public class CEO
    {
        private static string name;
        private static int age;

        public string Name { get => name; set => name = value; }
        public int Age { get=> age; set => age = value; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}:{Age}";
        }
    }

    public class Program { 
        public static void Main(string[] args)
        {
            var ceo1 = new CEO();
            ceo1.Name = "Inmy";
            ceo1.Age = 54;

            var ceo2 = new CEO();
            //Console.WriteLine(ceo1.ToString());
            Console.WriteLine(ceo2.ToString());

            Console.ReadLine();
        }
    }

}
