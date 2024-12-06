namespace AOC2024.Utility;

public class SetupInputFile
{
  public IEnumerable<string> OpenFile(string path)
  {
    return File.ReadLines(path);
  }
  public string GetSolutionDirectory()
  {
    string currentDirectory = Directory.GetCurrentDirectory();
    var directoryInfo = new DirectoryInfo(currentDirectory);

    while (directoryInfo != null && !File.Exists(Path.Combine(directoryInfo.FullName, "AOC2024.sln")))
    {
      directoryInfo = directoryInfo.Parent;
    }

    return directoryInfo?.FullName;
  }
}