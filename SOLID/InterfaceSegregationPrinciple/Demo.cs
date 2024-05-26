namespace InterfaceSegregationPrinciple
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

  public interface IPrinter
  {
    void Print(Document d);
  }

  public interface IScanner
  {
    void Scan(Document d);
  }
  public interface IFax
  {
    void Fax(Document d);
  }

  public class IPhotoCopier : IPrinter, IScanner
  {
    public void Print(Document d)
    {
      throw new NotImplementedException();
    }

    public void Scan(Document d)
    {
      throw new NotImplementedException();
    }
  }

  public interface IMultiFunctionDevice : IPrinter, IScanner //.....
  {

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

  public class MultiFunctionMachine : IMultiFunctionDevice
  {
    private IPrinter printer;
    private IScanner scanner;

    public MultiFunctionMachine(IPrinter printer, IScanner scanner)
    {
        if(printer == null)
        {
          throw new ArgumentNullException(paramName: nameof(printer));
        }
        if (scanner == null)
        {
          throw new ArgumentNullException(paramName: nameof(scanner));
        }
        this.printer = printer; 
        this.scanner = scanner;
    }

    public void Print(Document d)
    {
      //decorator
      printer.Print(d);
    }

    public void Scan(Document d)
    {
      //decorator
      scanner.Scan(d);
    }


    //public void Print(Document d)
    //{
    //  throw new NotImplementedException();
    //}

    //public void Scan(Document d)
    //{
    //  throw new NotImplementedException();
    //}


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
