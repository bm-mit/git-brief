using CommandLine;

namespace GitBrief.Utils.ArgsParser;

public class Options
{
    [Option('g', "graph", Required = false)]
    public bool Graph { get; set; }
}