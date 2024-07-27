using Terminal.Gui;

namespace GitBrief.Views.Branch;

public class BranchListWindow : Window
{
    public BranchListWindow()
    {
        Title = "branch list";
        Add(new BranchListView());
    }
}