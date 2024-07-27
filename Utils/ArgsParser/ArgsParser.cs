using GitBrief.Commands.Branches;
using GitBrief.Commands.Graph;
using CommandLine;

namespace GitBrief.Utils.ArgsParser;

public static class ArgsParser
{
    public static void Parse(string[] args)
    {
        Parser.Default.ParseArguments<GraphOptions, BranchesOptions>(args)
            .WithParsed<GraphOptions>(_ => GraphCommands.Generate())
            .WithParsed<BranchesOptions>(_ => BranchesCommands.DoSomething());
    }
}