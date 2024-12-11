using AOC2024.Utility;

namespace AOC2024;

static internal partial class Program
{
  public static void Main(string[] args)
  {
    for (int day = 25; day > 0; day--)
    {
      try
      {
        string inputFilePath = TestFiles.GetInputData(day, "puzzleInput.txt");
        object? dayInstance =
          Activator.CreateInstance(Type.GetType($"AOC2024.Day{day}") ?? throw new InvalidOperationException());
        Console.WriteLine("");
        Console.Write("Day " + day + ":");
        var startTime = DateTime.Now;
        Console.Write(dayInstance.GetType().GetMethod("Process").Invoke(dayInstance, new object[] { inputFilePath }));
        Console.Write("  Time: " + DateTime.Now.Subtract(startTime));
      }
      catch (InvalidOperationException)
      {
        //No Day# code yet
        Console.Write(".");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"An error occurred: {ex.Message}");
        break;
      }
    }
  }
}