using GitBrief.Utils.GitWrapper;
using InteractiveConsole;

namespace GitBrief.Commands.Branch;

public static class BranchCommands
{
    public static void List(BranchOptions.ListOptions options)
    {
        new SelectionPrompt<LibGit2Sharp.Branch>(
            items: GitBranch.ListLocalBranches(options.Path),
            converter:
            branch =>
                $"{branch.FriendlyName} -> {branch.CanonicalName.Replace("refs/head", branch.RemoteName)}"
        ).Show();
    }
}