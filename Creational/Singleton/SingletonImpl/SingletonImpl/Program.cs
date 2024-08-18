using MoreLinq;
using NUnit.Framework;

namespace DesignPatterns
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount; //0
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            //if (Instance == null)
            //{
            instanceCount++;
                Console.WriteLine("Initializing DB");
                capitals = File.ReadLines("capitals.txt")
                    .Batch(2)
                    .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
            //}
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(()=> new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;

    }

    //UNIT TESTS - START
    //[TestFixture]
    //public class SingletonTests
    //{
    //    [Test]
    //    public void IsSingletonTest()
    //    {
    //        var db = SingletonDatabase.Instance;
    //        var db2 = SingletonDatabase.Instance;
    //        Assert.That(db, Is.SameAs(db2));
    //        Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
    //    }
    //}
    //UNIT TESTS - END

    public class Program
    {
        public static void Main(string[] args)
        {
            /*var db = new SingletonDatabase.Instance;
            var countryName = "Tokyo";
            Console.WriteLine($"The population of {countryName} is " + db.GetPopulation(countryName));

            var db1 = new SingletonDatabase();
            var countryName1 = "New York";
            Console.WriteLine($"The population of {countryName1} is " + db.GetPopulation(countryName1));*/

            //var db = SingletonDatabase.Instance;
            //var countryName = "Tokyo";
            //Console.WriteLine($"The population of {countryName} is " + db.GetPopulation(countryName));

            //var db1 = SingletonDatabase.Instance;
            //var countryName1 = "New York";
            //Console.WriteLine($"The population of {countryName1} is " + db.GetPopulation(countryName1));

            //Singleton with Lazy
            var db = SingletonDatabase.Instance;
            var countryName = "Tokyo";
            Console.WriteLine($"The population of {countryName} is " + db.GetPopulation(countryName));

            Console.ReadKey();
        }

        

    }

   


}