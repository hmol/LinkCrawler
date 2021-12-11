
namespace LinkCrawler.Domain.Utils.Helpers;

public static class ConsoleHelper
{
    public static void WriteError(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
