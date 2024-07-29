namespace InteractiveConsole;

public class SelectionItem<T> : ISelectionItem<T> where T : notnull
{
    public SelectionItem(T item, string? representString)
    {
        Item = item;
        RepresentString = representString ?? Item.ToString() ?? "null";
    }

    public SelectionItem(T item, Converter<T, string>? converter = null) :
        this(item,
            (converter ?? (item => item.ToString() ?? ""))(item)
        )
    {
    }

    public (int, int)? CursorPosition { get; internal set; }
    public T Item { get; init; }
    public string RepresentString { get; init; }

    public void Render(ConsoleColor color)
    {
        CursorPosition ??= Console.GetCursorPosition();

        Console.SetCursorPosition(CursorPosition.Value.Item1, CursorPosition.Value.Item2);
        Console.ForegroundColor = color;
        Console.WriteLine(RepresentString);
        Console.ResetColor();
    }
}