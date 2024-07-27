using CommandLine;
using GitBrief.Commands.Graph;
using GitBrief.Utils.ArgsParser;

namespace GitBrief;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        var result = Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                if (options.Graph)
                    GraphGenerator.Generate();
                else
                    Console.WriteLine("No command provided!");
            })
            .WithNotParsed(_ => Console.Write("Error"));
    }
}