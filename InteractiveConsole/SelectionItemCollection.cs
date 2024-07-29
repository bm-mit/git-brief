using System.Collections;

namespace InteractiveConsole;

public class SelectionItemCollection<T> : ISelectionItemCollection<T>
    where T : notnull
{
    public SelectionItemCollection(T[] items, Converter<T, string>? converter = null)
    {
        Items = new SelectionItem<T>[items.Length];
        for (var i = 0; i < items.Length; ++i)
            Items[i] = new SelectionItem<T>(items[i], converter);
    }

    public SelectionItem<T> this[int index] => Items[index];
    public SelectionItem<T>[] Items { get; init; }
    public int SelectionIndex { get; } = 0;
    public int Length => Items.Length;

    public IEnumerator<ISelectionItem<T>> GetEnumerator()
    {
        return (IEnumerator<ISelectionItem<T>>)Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}