using System.Drawing;
using CommandLine;
using GitBrief.Commands.Graph;
using GitBrief.Utils.ArgsParser;
using Console = Colorful.Console;

namespace GitBrief;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        ArgsParser.Parse(args);
        
        Console.WriteLine("This is [color=Red]red[/color], [color=Green]green[/color], and [color=Blue]blue[/color].");
    }
}