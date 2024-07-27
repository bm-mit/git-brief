using GitBrief.Utils.GitWrapper;
namespace GitBrief.Commands.Branch;

public static class BranchCommands
{
    public static void List(BranchOptions.ListOptions options)
    {
        LibGit2Sharp.Branch[] branches = GitBranch.ListLocalBranches(options.Path);

        Console.WriteLine(BranchListView.Show(branches).FriendlyName);
    }
}