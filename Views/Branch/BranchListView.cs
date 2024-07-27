using System.Data;
using Terminal.Gui;

namespace GitBrief.Views.Branch;

public class BranchListView : TableView
{
    public BranchListView()
    {
        Table = new DataTableSource(new DataTable());
    }
}