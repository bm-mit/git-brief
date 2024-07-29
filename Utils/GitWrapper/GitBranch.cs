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

    private static Branch[] GetBranches(Repository repo, Func<Branch, bool>? filter = null)
    {
        filter ??= _ => true;
        return repo.Branches.Where(filter).ToArray();
    }

    public static Branch[] List(Repository repo)
    {
        var branches = GetBranches(repo);
        SortBranchList(ref branches);

        return branches;
    }

    public static void Checkout(string? directory, Branch? toBranch)
    {
        if (toBranch == null)
            return;

        var repo = GitRepo.Init(directory);

        try
        {
            LibGit2Sharp.Commands.Checkout(repo, toBranch);
        }
        catch (CheckoutConflictException e)
        {
            Console.WriteLine($"ERROR: {e.Message}");
        }
    }

    public static Branch[] ListLocalBranches(Repository repo)
    {
        var branches = GetBranches(repo, branch => !branch.IsRemote);
        SortBranchList(ref branches);

        return branches;
    }

    public static string GetFullRemoteName(Branch branch)
    {
        var remoteName = branch.RemoteName;
        var remoteBranchName = branch.CanonicalName.Replace("refs/heads", remoteName);

        return remoteBranchName;
    }

    public static Branch GetCurrentBranch(Repository repo)
    {
        return repo.Head;
    }

    public static bool IsCurrentBranch(Branch b, Repository repo)
    {
        return b.FriendlyName == GetCurrentBranch(repo).FriendlyName;
    }
}