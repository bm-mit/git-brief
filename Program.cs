using GitBrief.Utils.ArgsParser;

namespace GitBrief;

internal abstract class Program
{
    private static void Main(string[] args)
    {
        ArgsParser.Parse(args);
    }
}