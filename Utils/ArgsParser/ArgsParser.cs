using CommandLine;
using GitBrief.Commands.Branch;
using GitBrief.Commands.Graph;

namespace GitBrief.Utils.ArgsParser;

public static class ArgsParser
{
    public static void Parse(string[] args)
    {
        Parser.Default.ParseArguments<GraphOptions, BranchOptions>(args)
            .WithParsed<GraphOptions>(_ => GraphCommands.Generate())
            .WithParsed<BranchOptions>(BranchParser.Parse);
    }
}