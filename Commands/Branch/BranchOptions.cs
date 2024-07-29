using System.ComponentModel.DataAnnotations;
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
    
    [Verb("new", HelpText = "Create (checkout) new branch from current branch")]
    public class NewOptions
    {
        [Value(0, HelpText = "Path to git repository, using PWD if omit.", Required=false)]
        public string? Path { get; set; }
        
        [Value(1, HelpText = "New branch name, display a prompt to input if omit.", Required = false)]
        public string? BranchName { get; set; }
    }
}