using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Reflection;

namespace LanguageAndFramework;

[TestFixture]
public class ReflectionTestFixture
{
    [Test]
    public void CompareNativeAndReflection()
    {
        const int max = 1_000_000;

        var stopWatch = Stopwatch.StartNew();
        for (int i = 0; i < max; i++)
        {
            var sw = new Stopwatch();
            sw.Reset();
        }
        stopWatch.Stop();
        Console.WriteLine("Elapsed time of native:\n\t" + stopWatch.Elapsed);

        stopWatch.Restart();
        for (int i = 0; i < max; i++)
        {
            var sw = Activator.CreateInstance(typeof(Stopwatch)) as Stopwatch;
            sw.Reset();
        }
        stopWatch.Stop();
        Console.WriteLine("Elapsed time of reflection with type:\n\t" + stopWatch.Elapsed);

        stopWatch.Restart();
        for (int i = 0; i < max; i++)
        {
            var sw = Assembly.Load("System.Runtime").CreateInstance("System.Diagnostics.Stopwatch") as Stopwatch;
            sw.Reset();
        }
        stopWatch.Stop();
        Console.WriteLine("Elapsed time of reflection with assembly:\n\t" + stopWatch.Elapsed);
    }
}