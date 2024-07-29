using GitBrief.Utils.GitWrapper;
using InteractiveConsole;

namespace GitBrief.Commands.Branch;

public static class BranchCommands
{
    public static void List(BranchOptions.ListOptions options)
    {
        new SelectionPrompt<LibGit2Sharp.Branch>(
            items: GitBranch.ListLocalBranches(options.Path),
            title: "Switch branch: ",
            converter: branch =>
                branch.RemoteName != null ? 
                $"{branch.FriendlyName} -> {branch.CanonicalName.Replace("refs/heads", branch.RemoteName)}":
                branch.FriendlyName
        ).Show();
    }
}