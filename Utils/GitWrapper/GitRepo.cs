using LibGit2Sharp;

namespace GitBrief.Utils.GitWrapper;

public static class GitRepo
{
    public static Repository Init(string? directory)
    {
        return new Repository(directory ?? Directory.GetCurrentDirectory());
    }   
}