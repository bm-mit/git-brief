using GitBrief.Utils.GitWrapper;

namespace GitBrief.Commands.Branch;

public static class BranchCommands
{
    public static void List(BranchOptions.ListOptions options)
    {
        string? directory = options.Path;
        var branches = GitBranch.List(directory);

        foreach (var b in branches.Where(b => !b.IsRemote))
        {
            string prefix = GitBranch.IsCurrentBranch(b, directory) ? " * " : "   ";

            Console.WriteLine(
                b.IsTracking
                    ? $"{prefix}{b.FriendlyName} -> {GitBranch.GetFullRemoteName(b)}"
                    : $"{prefix}{b.FriendlyName}");
        }
    }
}