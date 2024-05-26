﻿namespace InterfaceSegregationPrinciple
{
  public class Document
  {

  }

  public interface IMachine
  {
    void Print(Document d);
    void Scan(Document d);
    void Fax(Document d);
  }

  public class MultiFunctionPrinter : IMachine
  {
    public void Fax(Document d)
    {
      throw new NotImplementedException();
    }

    public void Print(Document d)
    {
      throw new NotImplementedException();
    }

    public void Scan(Document d)
    {
      throw new NotImplementedException();
    }
  }

  public class OldFashionedPrinter : IMachine
  {
    public void Fax(Document d)
    {
      throw new NotImplementedException();
    }

    public void Print(Document d)
    {
      throw new NotImplementedException();
    }

    public void Scan(Document d)
    {
      throw new NotImplementedException();
    }
  }

  public class Demo
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello, World!");
      Console.ReadLine();
    }
  }
}
