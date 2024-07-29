namespace InteractiveConsole;

public interface ISelectionItemCollection<T> : IEnumerable<ISelectionItem<T>> where T : notnull
{
    public SelectionItem<T>[] Items { get; init; }
    public int SelectionIndex { get; }
    public int Length { get; }
}