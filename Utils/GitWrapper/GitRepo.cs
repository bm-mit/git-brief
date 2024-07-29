using LibGit2Sharp;

namespace GitBrief.Utils.GitWrapper;

public static class GitRepo
{
    public static Repository? Init(string? directory)
    {
        try
        {
            return new Repository(directory ?? Directory.GetCurrentDirectory());
        }
        catch (RepositoryNotFoundException e)
        {
            Console.WriteLine($"ERROR: {e.Message}");
        }

        return default;
    }

    public static Branch? CreateBranch(Repository repo, string branchName)
    {
        try
        {
            return repo.CreateBranch(branchName);
        }
        catch (NotFoundException)
        {
            Console.WriteLine("ERROR: Repository initialized without any commit. Please commit and retry.");
            return default;
        }
    }
}