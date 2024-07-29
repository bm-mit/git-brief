using System.Collections;

namespace InteractiveConsole;

public class SelectionItemCollection<T> : ISelectionItemCollection<T>
    where T : notnull
{
    public SelectionItem<T>[] Items { get; init; }
    public int SelectionIndex { get; private set; } = 0;
    public int Length => Items.Length;

    public SelectionItemCollection(T[] items, Converter<T, string>? converter = null)
    {
        Items = new SelectionItem<T>[items.Length];
        for (int i = 0; i < items.Length; ++i)
            Items[i] = new SelectionItem<T>(items[i], converter);
    }

    public IEnumerator<ISelectionItem<T>> GetEnumerator()
    {
        return (IEnumerator<ISelectionItem<T>>)Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public SelectionItem<T> this[int index] => Items[index];
}