using System;
using System.Threading.Tasks;

Console.WriteLine("Hello World!");

ShowArgs(args);
await RunningInATaskAsync();
Console.WriteLine("bye");

void ShowArgs(string[] args)
{
    foreach (var arg in args)
    {
        Console.WriteLine(arg);
    }
}

Task RunningInATaskAsync()
{
    return Task.Run(() => Console.WriteLine($"in a task: {Task.CurrentId}"));
}