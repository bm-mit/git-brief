using GitBrief.Utils.GitWrapper;
using InteractiveConsole;

namespace GitBrief.Commands.Branch;

public static class BranchCommands
{
    private static readonly Converter<LibGit2Sharp.Branch, string> Converter =
        branch => branch.RemoteName != null
            ? $"{branch.FriendlyName} -> {branch.CanonicalName.Replace("refs/heads", branch.RemoteName)}"
            : branch.FriendlyName;

    public static void List(BranchOptions.ListOptions options)
    {
        var directory = options.Path;
        var selectedBranch = new SelectionPrompt<LibGit2Sharp.Branch>(
            items: GitBranch.ListLocalBranches(directory),
            title: "Switch branch: ",
            converter: Converter
        ).Show();

        GitBranch.Checkout(directory, selectedBranch);
    }
}