using GitBrief.Utils.GitWrapper;
using InteractiveConsole;
using LibGit2Sharp;

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
        var repo = GitRepo.Init(directory);
        if (repo == null)
            return;

        var selectedBranch = new SelectionPrompt<LibGit2Sharp.Branch>(
            items: GitBranch.ListLocalBranches(repo),
            title: "Switch branch: ",
            converter: Converter
        ).Show();

        GitBranch.Checkout(directory, selectedBranch);
    }


    public static void New(BranchOptions.NewOptions options)
    {
        var directory = options.Path;

        var repo = GitRepo.Init(directory);
        if (repo == null)
            return;

        string branchName = options.BranchName ?? new InputPrompt("New branch name").Show();
        var branch = GitRepo.CreateBranch(repo, branchName);
        if (branch != null)
            LibGit2Sharp.Commands.Checkout(repo, branch);
    }
}