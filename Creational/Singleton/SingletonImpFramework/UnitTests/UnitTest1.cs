using Autofac;
using NUnit.Framework;
using SingletonImpFramework;
using System.Collections.Generic;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void Test1()
        //{
        //    Assert.Pass();
        //}

        [TestFixture]
        public class SingletonTests
        {


            [Test]
            public void IsSingletonTest()
            {
                var db = SingletonDatabase.Instance;
                var db2 = SingletonDatabase.Instance;
                Assert.That(db, Is.SameAs(db2));
                Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
            }

            [Test]
            public void SingletonPopulationTest()
            {
                var rf = new SingletonRecordFinder();
                var names = new[] { "Seoul", "Mexico City" };
                int tp = rf.GetTotalPopulation(names);
                Assert.That(tp, Is.EqualTo(17500000 + 17400000));
            }

            [Test]
            public void ConfigurablePopulationTest()
            {
                var rf = new ConfigurableRecordFinder(new DummyDatabase());
                var names = new[] { "alpha", "gamma" };
                int tp = rf.GetTotalPopulation(names);
                Assert.That(tp, Is.EqualTo(4));
            }

            [Test]
            public void DIPopulationTest()
            {
                var cb = new ContainerBuilder();
                cb.RegisterType<OrdinaryDatabase>()
                    .As<IDatabase>()
                    .SingleInstance();
                cb.RegisterType<ConfigurableRecordFinder>();

                using (var c = cb.Build()) {
                    var rf = c.Resolve<ConfigurableRecordFinder>();

                    var namesList = new[] { "Tokyo", "Mexico City" };
                    int tp = rf.GetTotalPopulation(namesList);
                    Assert.That(tp, Is.EqualTo(33200000 + 17400000));

                }
            }
        }
    }
}