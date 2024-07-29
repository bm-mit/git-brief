using System.Collections;

namespace InteractiveConsole;

public class SelectionItemCollection<T> : ISelectionItemCollection<T>
    where T : notnull
{
    public List<SelectionItem<T>> Items { get; init; } = [];
    public int SelectionIndex { get; private set; } = 0;
    public int Length => Items.Count;

    public SelectionItemCollection(T[] items, Converter<T, string>? converter = null)
    {
        foreach (var item in items)
            _ = Items.Append(new SelectionItem<T>(item, converter));
    }

    public IEnumerator<ISelectionItem<T>> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}