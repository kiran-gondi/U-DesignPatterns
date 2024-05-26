using System.Linq;

namespace DependencyInversionPrinciple
{
  public enum Relationship
  {
    Parent,
    Children,
    Sibling
  }

  public class Person
  {
      public string Name { get; set; }
  }

  public interface IRelationshipBrowser
  {
    IEnumerable<Person> FindAllChildrenOf(string name);
  }

  //low-level
  public class Relationships : IRelationshipBrowser
  {
    private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

    public void AddParentAndChild(Person parent, Person child)
    {
      relations.Add((parent, Relationship.Parent, child));
      relations.Add((child, Relationship.Children, parent));
    }

    public IEnumerable<Person> FindAllChildrenOf(string name)
    {
      return from rel in relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent)
             select rel.Item3;

      //foreach (var rel in relations.Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent))
      //{
      //  //Console.WriteLine($"John has a child called {rel.Item3.Name}");
      //  yield return rel.Item3;
      //}
    }

    //public List<(Person, Relationship, Person)> Relations => relations;
  }

  public class Resarch 
  {
    /*public Resarch(Relationships relationships)
    {
      var relations = relationships.Relations;
      foreach (var rel in relations.Where(x=>x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
      {
        Console.WriteLine($"John has a child called { rel.Item3.Name }");
      }
    }*/

    public Resarch(IRelationshipBrowser browser)
    {
      foreach (var item in browser.FindAllChildrenOf("John"))
      {
        Console.WriteLine($"John has a child called {item.Name}");
      }
    }
    static void Main(string[] args)
    {
      var parent = new Person { Name = "John" };
      var child1 = new Person { Name = "Chris" };
      var child2 = new Person { Name = "Mary" };

      var relationships = new Relationships();
      relationships.AddParentAndChild(parent, child1);
      relationships.AddParentAndChild(parent, child2);

      new Resarch(relationships);

      Console.ReadLine();
    }
  }
}
