using GitBrief.Utils.GitWrapper;
using LibGit2Sharp;
using Spectre.Console;

public static class BranchListView
{
    static string BranchToOptionString(Branch branch)
    {
        return branch.IsTracking
            ? $"{branch.FriendlyName} -> {GitBranch.GetFullRemoteName(branch)}"
            : $"{branch.FriendlyName}";
    }

    public static Branch Show(Branch[] branches)
    {
        SelectionPrompt<Branch> prompt = new();
        prompt.Title("Switch branch: ");
        prompt.AddChoices(branches);
        prompt.Converter = BranchToOptionString;

        return AnsiConsole.Prompt(prompt);
    }
}