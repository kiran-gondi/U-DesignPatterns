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

  //low-level
  public class Relationships
  {
    private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

    public void AddParentAndChild(Person parent, Person child)
    {
      relations.Add((parent, Relationship.Parent, child));
      relations.Add((child, Relationship.Children, parent));
    }

    public List<(Person, Relationship, Person)> Relations => relations;
  }

  public class Resarch
  {
    public Resarch(Relationships relationships)
    {
      var relations = relationships.Relations;
      foreach (var rel in relations.Where(x=>x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
      {
        Console.WriteLine($"John has a child called { rel.Item3.Name }");
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
  //internal class Program
  //{
  //  //static void Main(string[] args)
  //  //{
  //  //  Console.WriteLine("Hello, World!");
  //  //}
  //}
}
