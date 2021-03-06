using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LabWork1Back
{
  public static class RandomStringGenerator
  {
    private static Random random = new Random();

    public static string RandomString()
    {
      int length = random.Next(10, 30);
      const string chars = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
  }

  public class BenchmarkResult
  {
    public string timeElapsed { get; set; }
    public int N { get; set; }
    public long MemoryUsed { get; set; }
    public BenchmarkResult(string _timeElapsed, int n, long memoryUsed)
    {
      timeElapsed = _timeElapsed;
      N = n;
      MemoryUsed = memoryUsed;
    }
  }

  public static class Benchmark
  {
    static Random random = new Random();

    static string benchmarkDBPath = "benchmark.db";

    public static IEnumerable<BenchmarkResult> Run()
    {
      if (File.Exists(benchmarkDBPath))
        File.Delete(benchmarkDBPath);
      var results = new List<BenchmarkResult>();
      var _context = new FileDBContext(benchmarkDBPath, DBFileType.BinaryFile);
      GC.Collect();
      results.Add(RunTestsForDifferentN(_context));
      File.Delete(benchmarkDBPath);

      _context = new FileDBContext(benchmarkDBPath, DBFileType.JsonTextFile);
      GC.Collect();
      results.Add(RunTestsForDifferentN(_context));
      File.Delete(benchmarkDBPath);

      return results;
    }

    static BenchmarkResult RunTestsForDifferentN(IDBApiContext _context)
    {
      Stopwatch stopWatch = new Stopwatch();
      int N = 30_000;
      do
      {
        GC.Collect();
        stopWatch.Start();
        Test(_context, N);
        stopWatch.Stop();
        N += 10_000;
      }
      while (stopWatch.Elapsed.Seconds < 10);
      TimeSpan ts = stopWatch.Elapsed;
      string elapsedTime = String.Format("{0:00}.{1:00}",
        ts.Seconds, ts.Milliseconds / 10);
      return new BenchmarkResult(elapsedTime, N, (new FileInfo(benchmarkDBPath)).Length);
    }

    static void Test(IDBApiContext _context, int N)
    {
      for (int i = 0; i < N; i++)
        _context.AddMessage(GetRandomMessage());
      _context.SaveChanges();
      _context.LoadData();
    }

    public static Message GetRandomMessage()
    {
      return new Message()
      {
        Content = RandomStringGenerator.RandomString(),
        MessageType = (MessageTypeEnum)random.Next(5),
        SenderID = random.Next(int.MaxValue),
        ReceiverID = random.Next(int.MaxValue),
        TimeStamp = DateTime.Now,
        SpamScore = (ushort)random.Next(100)
      };
    }
  }
}
