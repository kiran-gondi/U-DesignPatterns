using MoreLinq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SingletonImpFramework
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
            capitals = File.ReadLines(Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"))
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

        private static Lazy<SingletonDatabase> instance = 
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;

    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names) {
            int result = 0;
            foreach (string name in names) {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    public class ConfigurableRecordFinder
    {
        private IDatabase database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (string name in names)
            {
                result += database.GetPopulation(name);
            }
            return result;
        }

    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>()
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        public OrdinaryDatabase()
        {
            //if (Instance == null)
            //{
            Console.WriteLine("Initializing DB");
            capitals = File.ReadLines(Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"))
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
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var countryName = "Tokyo";
            Console.WriteLine($"The population of {countryName} is " + db.GetPopulation(countryName));

            Console.ReadKey();
        }
    }
}
