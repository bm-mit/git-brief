using CommandLine;
using GitBrief.Commands.Branch;

namespace GitBrief.Utils.ArgsParser;

internal static class BranchParser
{
    public static void Parse(BranchOptions options)
    {
        string[] args = Environment.GetCommandLineArgs().Skip(2).ToArray();
        Parser.Default.ParseArguments<BranchOptions.ListOptions>(args)
            .WithParsed<BranchOptions.ListOptions>(BranchCommands.List);
    }
}