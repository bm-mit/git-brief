using CommandLine;

namespace GitBrief.Commands.Branch;

[Verb("branch", HelpText = "Do something with branches")]
public class BranchOptions
{
    [Verb("list", isDefault: true)]
    public class ListOptions
    {
        [Value(2, HelpText = "Path to git repository, using PWD if omit.", Default = true)]
        public string? Path { get; set; }
    }
}