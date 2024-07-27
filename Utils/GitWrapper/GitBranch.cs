using GitBrief.Commands.Branch;
using LibGit2Sharp;

namespace GitBrief.Utils.GitWrapper;

public static class GitBranch
{
    public static Branch[] List(string? directory)
    {
        var repo = GitRepo.Init(directory);
        Branch[] branches = repo.Branches.ToArray();
        Array.Sort(branches,
            (branch1, branch2) => branch1.IsRemote == branch2.IsRemote ? 0 :
                branch1.IsRemote ? -1 : 1);

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