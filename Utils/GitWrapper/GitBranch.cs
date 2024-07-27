using GitBrief.Commands.Branch;
using LibGit2Sharp;

namespace GitBrief.Utils.GitWrapper;

public static class GitBranch
{
    private static void SortBranchList(ref Branch[] branches)
    {
        Array.Sort(branches,
            (branch1, branch2) => branch1.IsRemote == branch2.IsRemote ? 0 :
                branch1.IsRemote ? -1 : 1);
    }

    private static Branch[] GetBranches(string? directory, Func<Branch, bool>? filter = null)
    {
        filter ??= _ => true;
        var repo = GitRepo.Init(directory);

        return repo.Branches.Where(filter).ToArray();
    }

    public static Branch[] List(string? directory)
    {
        Branch[] branches = GetBranches(directory);
        SortBranchList(ref branches);

        return branches;
    }

    public static Branch[] ListLocalBranches(string? directory)
    {
        Branch[] branches = GetBranches(directory, branch => !branch.IsRemote);
        SortBranchList(ref branches);

        return branches;
    }

    public static string GetFullRemoteName(Branch branch)
    {
        string remoteName = branch.RemoteName;
        string remoteBranchName = branch.CanonicalName.Replace("refs/heads", remoteName);

        return remoteBranchName;
    }

    public static Branch GetCurrentBranch(string? directory)
    {
        var repo = GitRepo.Init(directory);

        return repo.Head;
    }

    public static bool IsCurrentBranch(Branch b, string? directory)
    {
        return b.FriendlyName == GetCurrentBranch(directory).FriendlyName;
    }
}