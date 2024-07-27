using LibGit2Sharp;

namespace GitBrief.Utils.GitWrapper;

public static class GitBranch
{
    public static void List(string? directory)
    {
        Repository repo = GitRepo.Init(directory);
        
        foreach(Branch b in repo.Branches.Where(b => !b.IsRemote))
            Console.WriteLine(b.FriendlyName);
    }
}