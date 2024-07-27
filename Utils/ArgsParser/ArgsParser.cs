using GitBrief.Commands.Graph;
using CommandLine;
using GitBrief.Commands.Branch;

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