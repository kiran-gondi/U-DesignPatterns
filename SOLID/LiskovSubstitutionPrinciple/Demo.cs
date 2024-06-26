﻿namespace LiskovSubstitutionPrinciple
{
  public class Rectangle
  {
      public virtual int Height { get; set; }
      public virtual int Width { get; set; }

      public Rectangle()
      {
            
      }

      public Rectangle(int height, int width)
      {
        Height = height; 
        Width = width;
      }

      public override string ToString()
      {
        return $"{nameof(Height)}: {Height}, {nameof(Width)}: {Width}";
        //return $"Rectangle height is {Height} and widht is {Width}";
      }
  }

  public class Square : Rectangle { 
    public override int Width
    {
      set
      {
        base.Width = base.Height = value;
      }
    }

    public override int Height
    {
      set
      {
        base.Height = base.Width = value; 
      }
    }
  }


  public class Demo
  {
    static public int Area(Rectangle r) => r.Height * r.Width;

    static void Main(string[] args)
    {
      Rectangle rectangle = new Rectangle(3, 4);
      Console.WriteLine($"{rectangle} has area {Area(rectangle)}");

      Rectangle square = new Square();
      square.Width = 2;
      Console.WriteLine($"{square} has area {Area(square)}");

      Console.ReadLine();
    }
  }
}
