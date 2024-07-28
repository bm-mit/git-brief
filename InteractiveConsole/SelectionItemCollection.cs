using System.Collections;

namespace InteractiveConsole;

public class SelectionItemCollection<T> : ISelectionItemCollection<T> where T : notnull
{
    private SelectionItem<T>[] items;

    public SelectionItemCollection(T[] items)
    {
    }


    public void Render()
    {
    }

    public IEnumerator<T> GetEnumerator()
    {
        return (IEnumerator<T>)items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}