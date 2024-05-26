namespace OpenClosePrinciple
{
  internal class Program
  {
    public enum Color
    {
      Red,
      Green,
      Blue
    }

    public enum Size
    {
      Small,
      Medium,
      Large,
      Huge
    }

    public class Product
    {
      public string Name;
      public Color Color;
      public Size Size;

      public Product(string name, Color color, Size size)
      {
        if(name == null)
        {
          throw new ArgumentNullException(paramName: nameof(name));
        }
        Name = name; 
        Color = color; 
        Size = size;
      }
    }

    public class ProductFilter
    {
      // let's suppose we don't want ad-hoc queries on products
      public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
      {
        foreach (var product in products)
        {
          if (product.Color == color)
            yield return product;
        }
      }

      public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
      {
        foreach (var product in products)
        {
          if (product.Size == size)
            yield return product;
        }
      }

      public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
      {
        foreach (var product in products)
        {
          if (product.Size == size && product.Color == color)
            yield return product;
        }
      }// state space explosion
       // 3 criteria = 7 methods
       // OCP = open for extension but closed for modification
    }

    // we introduce two new interfaces that are open for extension
    public interface ISpecification<T>
    {
      bool IsSatisfiedBy(Product product);
    }

    public interface IFilter<T>
    {
      IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
      Color color;

      public ColorSpecification(Color color)
      {
        this.color = color;         
      }
      bool ISpecification<Product>.IsSatisfiedBy(Product product)
      {
        return product.Color == color;
      }
    }
    public class SizeSpecification : ISpecification<Product>
    {
      Size size;

      public SizeSpecification(Size size)
      {
        this.size = size;
      }
      bool ISpecification<Product>.IsSatisfiedBy(Product product)
      {
        return product.Size == size;
      }
    }

    // combinator
    public class AndSpecification<T> : ISpecification<T>
    {
      private ISpecification<T> first, second;

      public AndSpecification(ISpecification<T> first, ISpecification<T> second)
      {
        if (first == null) throw new ArgumentNullException(paramName: nameof(first));            
        if (second == null) throw new ArgumentNullException(paramName: nameof(second));       
        this.first = first; 
        this.second = second;
      }

      public bool IsSatisfiedBy(Product product)
      {
        return first.IsSatisfiedBy(product) && second.IsSatisfiedBy(product);
      }
    }

    public class BetterFilter : IFilter<Product>
    {
      public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
      {
        foreach (var item in items)
        {
          if(spec.IsSatisfiedBy(item))
            yield return item;
        }
      }
    }

    static void Main(string[] args)
    {
      var apple = new Product("Apple", Color.Green, Size.Small);
      var tree = new Product("Tree", Color.Green, Size.Large);
      var house = new Product("House", Color.Blue, Size.Large);

      Product[] products = { apple, tree, house };

      var productFilter = new ProductFilter();
      Console.WriteLine("Green products (old): ");
      foreach (var item in productFilter.FilterByColor(products, Color.Green))
      {
        Console.WriteLine($" - { item.Name } is green");
      }


      var betterFilter = new BetterFilter();
      Console.WriteLine("Green Products (new):");
      foreach (var item in betterFilter.Filter(products, new ColorSpecification(Color.Green)))
      {
        Console.WriteLine($" - {item.Name} is {Color.Green}");
      }

      Console.WriteLine("Large blue items");
      foreach (var item in betterFilter.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
      {
        Console.WriteLine($" - {item.Name} is big and blue");
      }
      
      Console.ReadKey();
    }
  }
}