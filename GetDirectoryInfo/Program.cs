using System.Diagnostics;
namespace GetDirectoryInfo;
internal class Program
{
    private static async Task Main(string[] args)
    {
        while (true)
        {
            await Console.Out.WriteAsync("Enter directory path: ");
            string path = Console.ReadLine();
            if (!Directory.Exists(path))
            {
                await Console.Out.WriteLineAsync("This is an empty or invalid directory path");
                continue;
            }
            await Console.Out.WriteLineAsync("Scanning...\n");
            MeasureElapsedTime(() => GetDirectoryInfo(path), out TimeSpan elapsedTime);
            Console.WriteLine("\nScanning completed.");
            await Console.Out.WriteLineAsync($"Elapsed Time: {elapsedTime}\n");
        }
    }

    private static void MeasureElapsedTime(Action action, out TimeSpan elapsedTime)
    {
        Stopwatch sw = Stopwatch.StartNew();
        action();
        elapsedTime = sw.Elapsed;
        sw.Reset();
    }

    private static void GetDirectoryInfo(string path)
    {
        DirectoryInfo directoryInfo = new(path);

        IEnumerable<FileInfo> fileInfos = directoryInfo.EnumerateFiles("*", new EnumerationOptions()
        { IgnoreInaccessible = true, RecurseSubdirectories = true });

        Console.WriteLine("******************* Top 10 Files With The Largest Size In The Specified Path. *******************\n");

        int i = 0;
        foreach (var fileInfo in fileInfos.OrderByDescending(fi => fi.Length).Take(10))
        {
            Console.WriteLine($"[{++i}] {fileInfo.FullName} -> Size: {fileInfo.Length / 1024D / 1024D} MB\n");
        }
    }
}