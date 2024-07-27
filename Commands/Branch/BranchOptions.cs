using CommandLine;

namespace GitBrief.Commands.Branch;

[Verb("branch", HelpText = "Do something with branches")]
public class BranchOptions
{
    [Verb("list")]
    public class ListOptions
    {
        [Value(0, HelpText = "Path to git repository, using PWD if omit.", Required = false)]
        public string? Path { get; set; }
    }
}