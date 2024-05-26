using System.Diagnostics;

namespace SingleResponsibilityPrinciple
{
  internal class Program
  {

    // just stores a couple of journal entries and ways of
    // working with them
    public class Journal
    {
      private readonly List<string> journalEntries = new List<string>();
      private static int count = 0;

      public int AddJournalEntry(string journalText)
      {
        journalEntries.Add($"{++count}: {journalText}");
        return count; //Memento Pattern
      }

      public void RemoveJournalEntry(int index)
      {
        journalEntries.RemoveAt( index );
      }

      public override string ToString() {
        return string.Join(Environment.NewLine, journalEntries);
      }

      // breaks single responsibility principle - START
      /*public void Save(string fileName, bool overwrite = false)
      {
        File.WriteAllLines(fileName, journalEntries);
      }

      public void Load(string fileName) { }

      public void Load(Uri uri) { }*/
      // breaks single responsibility principle - END

    }

    // handles the responsibility of persisting objects.
    public class Persistence
    {
      public void SaveToFile(Journal journal, string fileName, bool overWrite = false)
      {
        if(overWrite || !File.Exists(fileName))
        {
          File.WriteAllText(fileName, journal.ToString());
        }
      }
    }

    static void Main(string[] args)
    {
      var journal = new Journal();
      journal.AddJournalEntry("I practiced today.");
      journal.AddJournalEntry("I learned today.");
      journal.AddJournalEntry("I learned today1.");
      Console.WriteLine(journal.ToString());

      var journalPersistence = new Persistence();
      var fileName = @"C:\temp\journal.txt";
      journalPersistence.SaveToFile(journal, fileName, true);
      //Process.Start(fileName); //Throws an error in the MS Windows 11
      new Process() { StartInfo = new ProcessStartInfo(fileName) { UseShellExecute = true } }.Start();
    }
    
  }
}