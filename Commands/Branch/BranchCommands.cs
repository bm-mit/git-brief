using GitBrief.Utils.GitWrapper;

namespace GitBrief.Commands.Branch;

public static class BranchCommands
{
    public static void List(BranchOptions.ListOptions options)
    {
        GitBranch.List(options.Path);
    }
}